/*
 * Ext JS Library 2.0 RC 1
 * Copyright(c) 2006-2007, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://extjs.com/license
 */
 
 // Global variabel som innehåller själva gridden. 
 var globalStore;
 
 // Global variable som innehåller om användaren just har lagt till en rad. 
 var rowAdded = false;
 
 // Global variablel med den markerade aktiva raden.
 var activeRow = -1;
 
 // Global variabel som håller reda på om startplatsen på raden som modifieras nu är en ny eller inte. 
 var startLocationsRowEdited = false;
 
 // Global variabler som håller reda på vilket värde det stod i den nu modifierade startplatscellen. 
// var oldStartLocationValue = '';

 Ext.onReady(function() {
     Ext.QuickTips.init();

     // shorthand alias
     var fm = Ext.form;

     // Anger vilket index som områdesnummret skall ha
     var omradesnummer = 1;

     // the column model has information about grid columns
     // dataIndex maps the column to the specific data field in
     // the data store (created below)
     var cm = new Ext.grid.ColumnModel([{
         header: "Objektnr",
         dataIndex: 'objektnr',
         width: 80,
         align: 'right',
         editor: new fm.NumberField({
             allowBlank: false,
             allowNegative: false,
             maxValue: 100000
         })
     }, {
         id: 'startplats',
         header: "Startplats",
         dataIndex: 'startplats',
         width: 80,
         editor: new fm.TextField({
             allowBlank: false
         })
     }, {
         id: 'avdnr',
         header: "Avdelningsnr",
         dataIndex: 'avdnr',
         width: 100,
         editor: new fm.TextField({
             allowBlank: true
         })
     }, {
         id: 'avdnamn',
         header: "Avdelningsnamn",
         dataIndex: 'avdnamn',
         width: 150,
         editor: new fm.TextField({
             allowBlank: true
         })
     }, {
         header: "Areal ha",
         dataIndex: 'areal',
         width: 80,
         align: 'right',
         editor: new fm.NumberField({
             allowBlank: false,
             allowNegative: false,
             maxValue: 100000
         })
     }, {
         header: "Giva KgN/ha",
         dataIndex: 'giva',
         width: 80,
         align: 'right',
         editor: new fm.NumberField({
             allowBlank: false,
             allowNegative: false,
             maxValue: 100000
         })
     }, {
         header: "Skog-CAN ton",
         dataIndex: 'can',
         width: 80,
         align: 'right',
         editor: new fm.NumberField({
             allowBlank: false,
             allowNegative: false,
             maxValue: 100000
         })
     }, {
         header: "Kommentar",
         dataIndex: 'kommentar',
         width: 150,
         editor: new fm.TextField({
             allowBlank: true,
			 grow : false
         })
        }
    ]);

     // by default columns are sortable
     cm.defaultSortable = false;

     // this could be inline, but we want to define the Plant record
     // type so we can add records dynamically
     var OmrDef = Ext.data.Record.create([
     // the "name" below matches the tag name to read, except "availDate"
     // which is mapped to the tag "availability"
		   { name: 'objektnr', type: 'int' },
		   { name: 'startplats', type: 'string' },
		   { name: 'avdnr', type: 'string' },
           { name: 'avdnamn', type: 'string' },
		   { name: 'areal', type: 'float' },
		   { name: 'giva', type: 'int' },
		   { name: 'can', type: 'float' },
		   { name: 'kommentar', type: 'string' }
      ]);

     // create the Data Store
     globalStore = new Ext.data.Store({
         // load using HTTP
         url: 'Sweco.xml',

         // the return will be XML, so lets set up a reader
         reader: new Ext.data.XmlReader({
             // records will have a "plant" tag
             record: 'plant'
         }, OmrDef),

         sortInfo: { field: 'objektnr', direction: 'ASC' }
     });

     // create the editor grid
     var grid = new Ext.grid.EditorGridPanel({
         store: globalStore,
         cm: cm,
         renderTo: 'editor-grid',
         width: 840,
         height: 300,
         title: 'Objekt att gödsla',
         frame: true,
         clicksToEdit: 1,

         tbar: [
         // Lägger till knappen lägg till. 
			{
			// Anger texten på knappen. 
			text: 'Lägg till',

			// Lägger till vilka kolumner som skall vara med. 
			handler: function() {
			    var omrDef = new OmrDef({
			        objektnr: omradesnummer,
			        startplats: '',
			        avdnr: '',
			        avdnamn: '',
			        areal: '',
			        giva: 150,
			        can: '',
			        kommentar: ''
			    });

			    // Slutar med att redigering av tabellen. 
			    grid.stopEditing();

			    // Anger att en rad just har blivit tillagd och användaren får modifiera denna rad. 
			    rowAdded = true;

			    // Lägger till den nya raden. 
			    globalStore.insert(omradesnummer - 1, omrDef);

			    // Börjar med att redigera raden i kolumn ett, d.v.s. kolumnet till höger om Områdesnummret. 
			    grid.startEditing(omradesnummer - 1, 1);

			    // Ökar områdesnummret med ett till nästa gång. 
			    omradesnummer++;
			}

}, {
    // Anger texten på knappen. 
    text: 'Ta bort markerad rad',

    handler: function() {
        if (activeRow >= 0) {
            var response = window.confirm("Är du säker på att ta bort objekt " + (activeRow + 1) + "?");
            if (response) {
                var record = globalStore.getAt(activeRow);
                globalStore.remove(record);
                omradesnummer--;

                UppdateraObjektnummer(activeRow);
                TaBortStartplats(record.get('startplats'));
            }
        }
    }
}]
     });

     // trigger the data store load
     globalStore.load();

     // Lyssnar efter om användaren har skrivit in ett värde
     grid.on('afteredit', function(e) {
         // Justerar automatiskt startplatserna i gridden över startplatser. 
         if (e.column == 1) {
             if (startLocationsRowEdited) {
                 LäggTillStartplats(globalStore.getAt(e.row).get('startplats'));
                 startLocationsRowEdited = false;
             }
             else {
                 UppdateraStartplats(oldStartLocationValue, globalStore.getAt(e.row).get('startplats'));
             }
         }

         // Om användaren justerar värden i areal får endast en decimal finnas med. 
         if (e.column == 4) {
             // Hämtar värdet från gridden och tar bort alla decimaler utom en. 
             var record = globalStore.getAt(e.row);
             var areal = record.get('areal');
             areal = areal.toFixed(1);

             // Skriver tillbaka det nya värdet.
             record.set('areal', areal);
         }

         // Om användaren justerar värden i can får endast en decimal finnas med. 
         if (e.column == 6) {
             // Hämtar värdet från gridden och tar bort alla decimaler utom en. 
             var record = globalStore.getAt(e.row);
             var can = record.get('can');
             can = can.toFixed(1);

             // Skriver tillbaka det nya värdet.
             record.set('can', can);
         }

         // Räknar om summorna. 
         RäknaOm();
     }, this);

     // Kontrollerar så att användaren inte ändrar några celler som inte får ändras. 
     grid.on('beforeedit', function(e) {
         activeRow = e.row;

         // Kollar om användaren lägger till en ny rad
         if (omradesnummer - e.row == 1) {
             startLocationsRowEdited = true;
         }

         // Om användare ändrar värdet på en startplats.
         else if (e.column == 1) {
             oldStartLocationValue = globalStore.getAt(e.row).get('startplats');
         }

         // Kontollerar om användaren just har lagt till en ny rad, då får den modifieras annars ej. 
         if (rowAdded) {
             rowAdded = false;
             return;
         }

         // Kontrollerar så att användaren inte kan modifiera en rad som inte finns tillagd
         if (e.row > omradesnummer - 2) {
             e.cancel = true;
             return;
         }

         // Användaren får ej korrigera den första kolumnen
         if (e.column == 0) {
             e.cancel = true;
             return;
         }

     }, this);
	 
	 // När användaren trycker på en tanget
	 grid.on('keypress', function(e) {
		 
		 // Användaren skall inte kunna mata in fler än 30 tecken. 
		 if (e.column == 7) {
			var record = globalStore.getAt(e.row);
			var kommentar = record.get('kommentar');
			if (kommentar.length > 30) {
				kommentar = kommentar.substr(0,30);
				alert("För många tecken");
			}
			
			// Skriver tillbaka det nya värdet.
			record.set('kommentar', kommentar);
		 }
	 }, this);
 });

Ext.grid.CheckColumn = function(config){
    Ext.apply(this, config);
    if(!this.id){
        this.id = Ext.id();
    }
    this.renderer = this.renderer.createDelegate(this);
};

Ext.grid.CheckColumn.prototype ={
    init : function(grid){
        this.grid = grid;
        this.grid.on('render', function(){
            var view = this.grid.getView();
            view.mainBody.on('mousedown', this.onMouseDown, this);
        }, this);
    },

    onMouseDown : function(e, t){
        if(t.className && t.className.indexOf('x-grid3-cc-'+this.id) != -1){
            e.stopEvent();
            var index = this.grid.getView().findRowIndex(t);
            var record = this.grid.store.getAt(index);
            record.set(this.dataIndex, !record.data[this.dataIndex]);
        }
    },

    renderer : function(v, p, record){
        p.css += ' x-grid3-check-col-td'; 
        return '<div class="x-grid3-check-col'+(v?'-on':'')+' x-grid3-cc-'+this.id+'">&#160;</div>';
    }
};

//
// Egna funktioner
//

// Summerar ihop alla arealer. 
// return - En summa av alla arealer som är inskrivna. 
function SummaAreal()
{
	var summaAreal = 0;
	
	// Loopar igenom hela arealkolumnen och summerar ihop den. 
	if (startGlobalStore != null)
		for (var i = 0; i < startGlobalStore.getCount(); i++)
			summaAreal += startGlobalStore.getAt(i).get('areal') - 0;
	
	// Skriver tillbaka värdet med en decimal. 
	summaAreal = summaAreal.toFixed(1);
	document.formular.summaAreal.value = summaAreal;
}

// Summerar ihop alla CAN-er
// return - En summa av alla CAN-er som är inskrivna. 
function SummaCAN()
{
	var summaCAN = 0;
	
	// Loopar igenom hela arealkolumnen och summerar ihop den. 
	if (startGlobalStore != null)
		for (var i = 0; i < startGlobalStore.getCount(); i++)
			summaCAN += startGlobalStore.getAt(i).get('can') - 0;
	
	// Skriver tillbaka värdet med en decimal. 
	summaCAN = summaCAN.toFixed(1);
	document.formular.summaCAN.value = summaCAN;
}

// Uppdaterar alla objektnummer i gridden efter det borttagna objektets index. 
function UppdateraObjektnummer(Borttagningsindex)
{
	if (globalStore != null)
	{
		// Uppdatera alla kvarvarande objekts objektnummer efter det borttagna objektet. 
		for (var i = Borttagningsindex; i < globalStore.getCount() && i >= 0; i++)
		{
			// Uppdaterar objektnummret (med +1, ty store börjar räkna på noll och objekten på ett)
			var record = globalStore.getAt(i);
			record.set('objektnr', (i+1));
		}
	}
}

// Räknar om alla summa funktioner. 
function RäknaOm()
{
	StartRäknaOm();
	
	SummaAreal();
	SummaCAN();
}

// Avgör om denna grid är korrekt ifylld. 
function KorrektIFylld()
{
	var felUpptäckt = false;
	var fel = "";
	
	if (globalStore != null)
	{
		for (var i = 0; i < globalStore.getCount(); i++)
		{
			// Kontrollerar startplatsen
			if (globalStore.getAt(i).get('startplats') == '')
			{
				if (felUpptäckt)
					fel += '\n';
				
				fel += "Startplats (i Objekt att gödsla) på rad " + (i+1) + " är ej ifylld.";
				felUpptäckt = true;
			}
			
			// Kontrollerar arealen
			if (globalStore.getAt(i).get('areal') == '')
			{
				if (felUpptäckt)
					fel += '\n';
				
				fel += "Areal (i Objekt att gödsla) på rad " + (i+1) + " är ej ifylld.";
				felUpptäckt = true;
			}
			
			// Kontrollerar can
			if (globalStore.getAt(i).get('can') == '')
			{
				if (felUpptäckt)
					fel += '\n';
				
				fel += "Skog-CAN (i Objekt att gödsla) på rad " + (i+1) + " är ej ifylld.";
				felUpptäckt = true;
			}
		}
	}
	
	return fel;
}

// Hämtar alla objekt kopplade till en viss startplats.
function HämtaXML(Startplats)
{
	var xml = "";
	
	// Lägger in alla objekt vars startplats överrensstämmer
	for (var i = 0; i < globalStore.getCount(); i++)
	{
		// Om startplatsen överrensstämmer skall objektet läggas till. 
		if (globalStore.getAt(i).get('startplats') == Startplats)
		{
			xml += "\n\t\t<Objekt>";
				
			xml += "\n\t\t\t<Objektnr>" + globalStore.getAt(i).get('objektnr') + "</Objektnr>";
			xml += "\n\t\t\t<Avdnr>" + FixaTillTecken(globalStore.getAt(i).get('avdnr')) + "</Avdnr>";
			xml += "\n\t\t\t<Avdnamn>" + FixaTillTecken(globalStore.getAt(i).get('avdnamn')) + "</Avdnamn>";
			xml += "\n\t\t\t<Areal_ha>" + globalStore.getAt(i).get('areal') + "</Areal_ha>";
			xml += "\n\t\t\t<Giva_KgN_ha>" + globalStore.getAt(i).get('giva') + "</Giva_KgN_ha>";
			xml += "\n\t\t\t<CAN_ton>" + globalStore.getAt(i).get('can') + "</CAN_ton>";
			xml += "\n\t\t\t<Kommentar>" + FixaTillTecken(globalStore.getAt(i).get('kommentar')) + "</Kommentar>";
				
			xml += "\n\t\t</Objekt>";
		}
	}
	
	return xml;
}

function HämtaText(Startplats)
{
	var text = "";
	
	// Lägger in alla objekt vars startplats överrensstämmer
	for (var i = 0; i < globalStore.getCount(); i++)
	{
		// Om startplatsen överrensstämmer skall objektet läggas till. 
		if (globalStore.getAt(i).get('startplats') == Startplats)
		{				
			text += "\n\n\Objektnr: " + globalStore.getAt(i).get('objektnr');
			text += "\nAvdnr: " + globalStore.getAt(i).get('avdnr');
			text += "\nAvdnamn: " + globalStore.getAt(i).get('avdnamn');
			text += "\nAreal ha: " + globalStore.getAt(i).get('areal');
			text += "\nGiva KgN/ha: " + globalStore.getAt(i).get('giva');
			text += "\nCAN ton: " + globalStore.getAt(i).get('can');
			text += "\nKommentar: " + globalStore.getAt(i).get('kommentar');
		}
	}
	
	return text;
}

/**
 * Kollar om kommentarerna är korrekta. 
 *
 * @return - En array med alla fel i, om inga fel en tom array. 
 */
function KollaKommentarer()
{
	var errorIndecices = new Array();

	if (globalStore == null)
	{
		return errorIndecices;
	}

	for (var i = 0; i < globalStore.getCount(); i++)
	{
		if (globalStore.getAt(i).get('kommentar').length > 50)
		{
			errorIndecices.push((i+1));
		}
	}
	
	return errorIndecices;
}