/*
 * Ext JS Library 2.0 RC 1
 * Copyright(c) 2006-2007, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://extjs.com/license
 */
 
  // Global variabel som inneh�ller sj�lva gridden. 
 var RglobalStore;
 
  // Global variable som inneh�ller om anv�ndaren just har lagt till en rad. 
 var RrowAdded = false;
 
  // Global variablel med den markerade aktiva raden.
 var RactiveRow = -1;

Ext.onReady(function(){
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
           dataIndex: 'Robjektnr',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
               maxValue: 100000
           })
        },{
           id:'avdnr',
           header: "Avdelningsnr",
           dataIndex: 'Ravdnr',
           width: 100,
		   fixed: true,
           editor: new fm.TextField({
               allowBlank: true
           })
        },{
           id:'avdnamn',
           header: "Avdelningsnamn",
           dataIndex: 'Ravdnamn',
           width: 150,
		   fixed: true,
           editor: new fm.TextField({
               allowBlank: true
           })
        },{
		   header: "Areal ha",
           dataIndex: 'Rareal',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
               maxValue: 100000
           })
		},{
		   header: "Giva KgN/ha",
           dataIndex: 'Rgiva',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
               maxValue: 100000
           })
		},{
         header: "Kommentar",
         dataIndex: 'Rkommentar',
         width: 150,
         editor: new fm.TextField({
             allowBlank: true
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
		   {name: 'Robjektnr', type: 'int'},
		   {name: 'Ravdnr', type: 'string'},
           {name: 'Ravdnamn', type: 'string'},
		   {name: 'Rareal', type: 'float'}, 
		   {name: 'Rgiva', type: 'int'},
		   {name: 'Rkommentar', type: 'string'}
      ]);

    // create the Data Store
    var store = new Ext.data.Store({
        // load using HTTP
        url: 'Sweco.xml',

        // the return will be XML, so lets set up a reader
        reader: new Ext.data.XmlReader({
               // records will have a "plant" tag
               record: 'plant'
           }, OmrDef),

        sortInfo:{field:'Robjektnr', direction:'ASC'}
    });

    // create the editor grid
    var grid = new Ext.grid.EditorGridPanel({
        store: store,
        cm: cm,
        renderTo: 'editor-grid2',
        width:680,
        height:150,
        title:'Reservobjekt att eventuellt g�dsla',
        frame:true,
        clicksToEdit:1,

        tbar: [
			// L�gger till knappen l�gg till. 
			{
			// Anger texten p� knappen. 
            text: 'L�gg till',
			
			// L�gger till vilka kolumner som skall vara med. 
            handler : function(){
                var omrDef = new OmrDef({
					Robjektnr: 'R' + omradesnummer,
					Ravdnr: '',
                    Ravdnamn: '', 
					Rareal: '', 
					Rgiva: 150, 
					Rkommentar: ''
                });
				
				// Slutar med att redigering av tabellen. 
                grid.stopEditing();
				
				// Anger att en rad just har blivit tillagd och anv�ndaren f�r modifiera denna rad. 
				RrowAdded = true;
				
				// L�gger till den nya raden. 
				store.insert(omradesnummer-1, omrDef);
				
				// B�rjar med att redigera raden i kolumn ett, d.v.s. kolumnet till h�ger om Omr�desnummret. 
                grid.startEditing(omradesnummer-1, 1);
				
				// �kar omr�desnummret med ett till n�sta g�ng. 
				omradesnummer++;
				
				// Pekar om store s� att vi har den globalt. 
				RglobalStore = store;
				
				RSummaAreal();
			}
        },{
			// Anger texten p� knappen. 
            text: 'Ta bort markerad rad',
			
			handler : function()
			{
				if (RactiveRow >= 0)
				{
					var response = window.confirm("�r du s�ker p� att ta bort objekt " + (RactiveRow+1) + "?");
					if (response) 
					{
						var record = store.getAt(RactiveRow);
						RglobalStore.remove(record);
						//RglobalStore.commitChanges();
						omradesnummer--;
					
						RUppdateraObjektnummer(RactiveRow);
					}
				}
			}
		}]
    });

    // trigger the data store load
    store.load();
	
	// Lyssnar efter om anv�ndaren
	grid.on('afteredit', function (e)
	{
		// Om anv�ndaren justerar v�rden i areal f�r endast en decimal finnas med. 
		if (e.column == 3)
		{
			// H�mtar v�rdet fr�n gridden och tar bort alla decimaler utom en. 
			var record = RglobalStore.getAt(e.row);
			var areal = record.get('Rareal');
			areal = areal.toFixed(1);
			
			// Skriver tillbaka det nya v�rdet.
			record.set('Rareal', areal);
			//RglobalStore.commitChanges();
		}
		
        // R�knar om summorna. 
		RR�knaOm();
    }, this);
	
		// Kontrollerar s� att anv�ndaren inte �ndrar n�gra celler som inte f�r �ndras. 
	grid.on('beforeedit', function(e)
	{
		RactiveRow = e.row;
			
		// Kontollerar om anv�ndaren just har lagt till en ny rad, d� f�r den modifieras annars ej. 
		if (RrowAdded)
		{
			RrowAdded = false;
			return;
		}

		// Kontrollerar s� att anv�ndaren inte kan modifiera en rad som inte finns tillagd
		if (e.row > omradesnummer-2)
		{
			e.cancel = true;
			return;
		}
		
		// Anv�ndaren f�r ej korrigera den f�rsta kolumnen
		if (e.column == 0)
		{
			e.cancel = true;
			return;
		}
		
	}, this);
	
	 // N�r anv�ndaren trycker p� en tanget
	 grid.on('keypress', function(e) {
		 
		 // Anv�ndaren skall inte kunna mata in fler �n 30 tecken. 
		 if (e.column == 5) {
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
function RSummaAreal()
{
	var summaAreal = 0;
	
	// Loopar igenom hela arealkolumnen och summerar ihop den. 
	for (var i = 0; i < RglobalStore.getCount(); i++)
		summaAreal += RglobalStore.getAt(i).get('Rareal') - 0;
	
	// Skriver tillbaka 
	summaAreal = summaAreal.toFixed(1);
	document.formular.RsummaAreal.value = summaAreal;
}

// Uppdaterar alla objektnummer i gridden efter det borttagna objektets index. 
function RUppdateraObjektnummer(Borttagningsindex)
{
	if (RglobalStore != null)
	{
		// Uppdatera alla kvarvarande objekts objektnummer efter det borttagna objektet. 
		for (var i = Borttagningsindex; i < RglobalStore.getCount() && i >= 0; i++)
		{
			// Uppdaterar objektnummret (med +1, ty store b�rjar r�kna p� noll och objekten p� ett)
			var record = RglobalStore.getAt(i);
			record.set('Robjektnr', ('R' + (i+1)));
			//RglobalStore.commitChanges();
		}
		
		RR�knaOm();
	}
}

// R�knar om alla summa funktioner. 
function RR�knaOm()
{
	RSummaAreal();
}

// Kontrollerar om denna tabell �r korrekt ifylld. 
function RKorrektIFylld()
{
	var felUppt�ckt = false;
	var fel = "";
	
	if (RglobalStore != null)
	{
		// G�r igenom alla reservobjekt
		for (var i = 0; i < RglobalStore.getCount(); i++)
		{		
			// Kontrollerar arealen
			if (RglobalStore.getAt(i).get('Rareal') == '')
			{
				if (felUppt�ckt)
					fel += '\n';
				
				fel += "Areal (i Reservobjekt att eventuellt g�dsla) p� rad " + (i+1) + " �r ej ifylld.";
				felUppt�ckt = true;
			}
		}
	}
	
	return fel;
}

// H�mtar all data som en del till en XML-str�ng. 
function RH�mtaXML()
{
	var xml = "";

	// L�gger till alla reservobjekt. 
	if (RglobalStore != null)
	{
		for (var i = 0; i < RglobalStore.getCount(); i++)
		{
			xml += "\n\t<Reservobjekt>";
				
			xml += "\n\t\t<Objektnr>" + RglobalStore.getAt(i).get('Robjektnr') + "</Objektnr>";
			xml += "\n\t\t<Avdnr>" + FixaTillTecken(RglobalStore.getAt(i).get('Ravdnr')) + "</Avdnr>";
			xml += "\n\t\t<Avdnamn>" + FixaTillTecken(RglobalStore.getAt(i).get('Ravdnamn')) + "</Avdnamn>";
			xml += "\n\t\t<Areal_ha>" + RglobalStore.getAt(i).get('Rareal') + "</Areal_ha>";
			xml += "\n\t\t<Giva_KgN_ha>" + RglobalStore.getAt(i).get('Rgiva') + "</Giva_KgN_ha>";
			xml += "\n\t\t\t<Kommentar>" + FixaTillTecken(RglobalStore.getAt(i).get('Rkommentar')) + "</Kommentar>";
					
			xml += "\n\t</Reservobjekt>";
		}
	}
	
	return xml;
}

// H�mtar all data som text. 
function RH�mtaText()
{
    var text = "";

	// L�gger till alla reservobjekt. 
	if (RglobalStore != null)
	{
		// Kollar om vi har n�gra reservobjekt �verhuvudtaget. 
	    if (RglobalStore.getCount() > 0)
			text += "\n\nReservobjekt:";
		
		for (var i = 0; i < RglobalStore.getCount(); i++)
		{
			if (i > 0)
				text += "\n";
		
			text += "\nObjektnr: " + RglobalStore.getAt(i).get('Robjektnr');
			text += "\nAvdnr: " + RglobalStore.getAt(i).get('Ravdnr');
			text += "\nAvdnamn: " + RglobalStore.getAt(i).get('Ravdnamn');
			text += "\nAreal_ha: " + RglobalStore.getAt(i).get('Rareal');
			text += "\nGiva_KgN_ha: " + RglobalStore.getAt(i).get('Rgiva');
			text += "\nKommentar: " + RglobalStore.getAt(i).get('Rkommentar');
		}
	}
	
	return text;
}

/**
 * Kollar om kommentarerna �r korrekta. 
 *
 * @return - En array med alla fel i, om inga fel en tom array. 
 */
function RKollaKommentarer()
{
	var errorIndecices = new Array();
	
	if (RglobalStore == null)
	{
		return errorIndecices;
	}

	for (var i = 0; i < RglobalStore.getCount(); i++)
	{
		if (RglobalStore.getAt(i).get('Rkommentar').length > 50)
		{
			errorIndecices.push((i+1));
		}
	}
	
	return errorIndecices;
}