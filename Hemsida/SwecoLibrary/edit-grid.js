/*
 * Ext JS Library 2.0 RC 1
 * Copyright(c) 2006-2007, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://extjs.com/license
 */
 
 // Global variabel som inneh�ller sj�lva gridden. 
 var globalStore;
 
 // Global variable som inneh�ller om anv�ndaren just har lagt till en rad. 
 var rowAdded = false;
 
 // Global variablel med den markerade aktiva raden.
 var activeRow = -1;
 
 // Global variabel som h�ller reda p� om startplatsen p� raden som modifieras nu �r en ny eller inte. 
 var startLocationsRowEdited = false;
 
 // Global variabler som h�ller reda p� vilket v�rde det stod i den nu modifierade startplatscellen. 
// var oldStartLocationValue = '';

 Ext.onReady(function() {
     Ext.QuickTips.init();

     // shorthand alias
     var fm = Ext.form;

     // Anger vilket index som omr�desnummret skall ha
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
         title: 'Objekt att g�dsla',
         frame: true,
         clicksToEdit: 1,

         tbar: [
         // L�gger till knappen l�gg till. 
			{
			// Anger texten p� knappen. 
			text: 'L�gg till',

			// L�gger till vilka kolumner som skall vara med. 
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

			    // Anger att en rad just har blivit tillagd och anv�ndaren f�r modifiera denna rad. 
			    rowAdded = true;

			    // L�gger till den nya raden. 
			    globalStore.insert(omradesnummer - 1, omrDef);

			    // B�rjar med att redigera raden i kolumn ett, d.v.s. kolumnet till h�ger om Omr�desnummret. 
			    grid.startEditing(omradesnummer - 1, 1);

			    // �kar omr�desnummret med ett till n�sta g�ng. 
			    omradesnummer++;
			}

}, {
    // Anger texten p� knappen. 
    text: 'Ta bort markerad rad',

    handler: function() {
        if (activeRow >= 0) {
            var response = window.confirm("�r du s�ker p� att ta bort objekt " + (activeRow + 1) + "?");
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

     // Lyssnar efter om anv�ndaren har skrivit in ett v�rde
     grid.on('afteredit', function(e) {
         // Justerar automatiskt startplatserna i gridden �ver startplatser. 
         if (e.column == 1) {
             if (startLocationsRowEdited) {
                 L�ggTillStartplats(globalStore.getAt(e.row).get('startplats'));
                 startLocationsRowEdited = false;
             }
             else {
                 UppdateraStartplats(oldStartLocationValue, globalStore.getAt(e.row).get('startplats'));
             }
         }

         // Om anv�ndaren justerar v�rden i areal f�r endast en decimal finnas med. 
         if (e.column == 4) {
             // H�mtar v�rdet fr�n gridden och tar bort alla decimaler utom en. 
             var record = globalStore.getAt(e.row);
             var areal = record.get('areal');
             areal = areal.toFixed(1);

             // Skriver tillbaka det nya v�rdet.
             record.set('areal', areal);
         }

         // Om anv�ndaren justerar v�rden i can f�r endast en decimal finnas med. 
         if (e.column == 6) {
             // H�mtar v�rdet fr�n gridden och tar bort alla decimaler utom en. 
             var record = globalStore.getAt(e.row);
             var can = record.get('can');
             can = can.toFixed(1);

             // Skriver tillbaka det nya v�rdet.
             record.set('can', can);
         }

         // R�knar om summorna. 
         R�knaOm();
     }, this);

     // Kontrollerar s� att anv�ndaren inte �ndrar n�gra celler som inte f�r �ndras. 
     grid.on('beforeedit', function(e) {
         activeRow = e.row;

         // Kollar om anv�ndaren l�gger till en ny rad
         if (omradesnummer - e.row == 1) {
             startLocationsRowEdited = true;
         }

         // Om anv�ndare �ndrar v�rdet p� en startplats.
         else if (e.column == 1) {
             oldStartLocationValue = globalStore.getAt(e.row).get('startplats');
         }

         // Kontollerar om anv�ndaren just har lagt till en ny rad, d� f�r den modifieras annars ej. 
         if (rowAdded) {
             rowAdded = false;
             return;
         }

         // Kontrollerar s� att anv�ndaren inte kan modifiera en rad som inte finns tillagd
         if (e.row > omradesnummer - 2) {
             e.cancel = true;
             return;
         }

         // Anv�ndaren f�r ej korrigera den f�rsta kolumnen
         if (e.column == 0) {
             e.cancel = true;
             return;
         }

     }, this);
	 
	 // N�r anv�ndaren trycker p� en tanget
	 grid.on('keypress', function(e) {
		 
		 // Anv�ndaren skall inte kunna mata in fler �n 30 tecken. 
		 if (e.column == 7) {
			var record = globalStore.getAt(e.row);
			var kommentar = record.get('kommentar');
			if (kommentar.length > 30) {
				kommentar = kommentar.substr(0,30);
				alert("F�r m�nga tecken");
			}
			
			// Skriver tillbaka det nya v�rdet.
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
// return - En summa av alla arealer som �r inskrivna. 
function SummaAreal()
{
	var summaAreal = 0;
	
	// Loopar igenom hela arealkolumnen och summerar ihop den. 
	if (startGlobalStore != null)
		for (var i = 0; i < startGlobalStore.getCount(); i++)
			summaAreal += startGlobalStore.getAt(i).get('areal') - 0;
	
	// Skriver tillbaka v�rdet med en decimal. 
	summaAreal = summaAreal.toFixed(1);
	document.formular.summaAreal.value = summaAreal;
}

// Summerar ihop alla CAN-er
// return - En summa av alla CAN-er som �r inskrivna. 
function SummaCAN()
{
	var summaCAN = 0;
	
	// Loopar igenom hela arealkolumnen och summerar ihop den. 
	if (startGlobalStore != null)
		for (var i = 0; i < startGlobalStore.getCount(); i++)
			summaCAN += startGlobalStore.getAt(i).get('can') - 0;
	
	// Skriver tillbaka v�rdet med en decimal. 
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
			// Uppdaterar objektnummret (med +1, ty store b�rjar r�kna p� noll och objekten p� ett)
			var record = globalStore.getAt(i);
			record.set('objektnr', (i+1));
		}
	}
}

// R�knar om alla summa funktioner. 
function R�knaOm()
{
	StartR�knaOm();
	
	SummaAreal();
	SummaCAN();
}

// Avg�r om denna grid �r korrekt ifylld. 
function KorrektIFylld()
{
	var felUppt�ckt = false;
	var fel = "";
	
	if (globalStore != null)
	{
		for (var i = 0; i < globalStore.getCount(); i++)
		{
			// Kontrollerar startplatsen
			if (globalStore.getAt(i).get('startplats') == '')
			{
				if (felUppt�ckt)
					fel += '\n';
				
				fel += "Startplats (i Objekt att g�dsla) p� rad " + (i+1) + " �r ej ifylld.";
				felUppt�ckt = true;
			}
			
			// Kontrollerar arealen
			if (globalStore.getAt(i).get('areal') == '')
			{
				if (felUppt�ckt)
					fel += '\n';
				
				fel += "Areal (i Objekt att g�dsla) p� rad " + (i+1) + " �r ej ifylld.";
				felUppt�ckt = true;
			}
			
			// Kontrollerar can
			if (globalStore.getAt(i).get('can') == '')
			{
				if (felUppt�ckt)
					fel += '\n';
				
				fel += "Skog-CAN (i Objekt att g�dsla) p� rad " + (i+1) + " �r ej ifylld.";
				felUppt�ckt = true;
			}
		}
	}
	
	return fel;
}

// H�mtar alla objekt kopplade till en viss startplats.
function H�mtaXML(Startplats)
{
	var xml = "";
	
	// L�gger in alla objekt vars startplats �verrensst�mmer
	for (var i = 0; i < globalStore.getCount(); i++)
	{
		// Om startplatsen �verrensst�mmer skall objektet l�ggas till. 
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

function H�mtaText(Startplats)
{
	var text = "";
	
	// L�gger in alla objekt vars startplats �verrensst�mmer
	for (var i = 0; i < globalStore.getCount(); i++)
	{
		// Om startplatsen �verrensst�mmer skall objektet l�ggas till. 
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
 * Kollar om kommentarerna �r korrekta. 
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