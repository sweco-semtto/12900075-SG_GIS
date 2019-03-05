/*
 * Ext JS Library 2.0 RC 1
 * Copyright(c) 2006-2007, Ext JS, LLC.
 * licensing@extjs.com
 * 
 * http://extjs.com/license
 */
 
 // Global variabel som innehåller själva gridden. 
 var startGlobalStore;
 
 var startGlobalCm;
 
 // Global variabel som innehåller antalet tillagda startplatser i denna grid. 
 var startplatsNummer = 0;
 
 // Global variabel med områdesdefinitionen, 
 var startOmrDef;
 
 // Global varaibel som innehåller girdden. 
 var startGrid;

Ext.onReady(function(){
    Ext.QuickTips.init();

    // shorthand alias
    var fm = Ext.form;
	
	// Anger vilket index som områdesnummret skall ha
	var omradesnummer = 1;

    // the column model has information about grid columns
    // dataIndex maps the column to the specific data field in
    // the data store (created below)
    var cm = new Ext.grid.ColumnModel([{
           id:'startplats',
           header: "Startplats",
           dataIndex: 'startplats',
           width: 80,
		   fixed: true,
           editor: new fm.TextField({
               allowBlank: false
		   })
		},{
		   header: "Koord N",
           dataIndex: 'nkoord',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
			   minValue: 6126000,
               maxValue: 7693000
           })
		},{
		   header: "Koord Ö",
           dataIndex: 'okoord',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
			   minValue: 218000,
               maxValue: 1084000
           })
		},{
		   header: "Areal ha",
           dataIndex: 'areal',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
               maxValue: 100000
           })
		},{
		   header: "Skog-CAN ton",
           dataIndex: 'can',
           width: 80,
           align: 'right',
		   fixed: true,
           editor: new fm.NumberField({
               allowBlank: false,
               allowNegative: false,
               maxValue: 100000
           })
		}
    ]);

    // by default columns are sortable
    cm.defaultSortable = false;
	
	startGlobalCm = cm;

    // this could be inline, but we want to define the Plant record
    // type so we can add records dynamically
    startOmrDef = Ext.data.Record.create([
           // the "name" below matches the tag name to read, except "availDate"
           // which is mapped to the tag "availability"
		   {name: 'startplats', type: 'string'},
		   {name: 'nkoord', type: 'float'},
		   {name: 'okoord', type: 'float'},
		   {name: 'areal', type: 'float'}, 
		   {name: 'can', type: 'float'}
      ]);
	  
    // create the Data Store
    startGlobalStore = new Ext.data.Store({
        // load using HTTP
        url: 'Sweco.xml',

        // the return will be XML, so lets set up a reader
        reader: new Ext.data.XmlReader({
               // records will have a "plant" tag
               record: 'plant'
           }, startOmrDef),

        sortInfo:{field:'startplats', direction:'ASC'}
    });

	
    // create the editor grid
    startGrid = new Ext.grid.EditorGridPanel({
        store: startGlobalStore,
        cm: cm,
        renderTo: 'editor-grid3',
        width:440,
        height:200,
        title:'Startplatser',
        frame:true,
        clicksToEdit:1,
		tbar: []
	});

    // trigger the data store load
    startGlobalStore.load();
	
	// Kontrollerar så att användaren inte ändrar några celler som inte får ändras. 
	startGrid.on('beforeedit', function(e)
	{	
		// Användaren får ej korrigera den kolumnen startplats
		if (e.column == 0)
		{
			e.cancel = true;
			return;
		}
		
		// Användaren får ej korrigera den kolumnen areal
		if (e.column == 3)
		{
			e.cancel = true;
			return;
		}
		
		// Användaren får ej korrigera den kolumnen can
		if (e.column == 4)
		{
			e.cancel = true;
			return;
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

// Den abstrakta datatypen Startplats. 
function Startplats (Startplats, Nordligkoordinat, Ostligkoordinat, Areal)
{
	this.Startplats = Startplats;
	this.Nordligkoordinat = Nordligkoordinat;
	this.Ostligkoordinat = Ostligkoordinat;
	this.Areal = Areal;
}


// Lägger till en startplats i startgridden. 
function LäggTillStartplats(Startplats)
{
	// Sätter områdesdefinitionen till att vara global. 
	var omrDef = new startOmrDef(
	{
		startplats: '' + Startplats,
		nkoord: '',
        okoord: '', 
		areal: '', 
		can:''
	});
	
	// Kollar om denna startplats redan finns med. 
	for (var i = 0; i < startGlobalStore.getCount(); i++)
	{
		// Om startplatsens namn återfinns skall den inte läggas till. 
		if (startGlobalStore.getAt(i).get('startplats') == Startplats)
		{
			return;
		}
	}
	
	// Lägger till den nya raden. 
	startGlobalStore.insert(startplatsNummer, omrDef);
	startGlobalStore.commitChanges();
						
	// Ökar områdesnummret med ett till nästa gång. 
	startplatsNummer++;
}

// Tar bort en startplats ur gridden. 
function TaBortStartplats(Startplats)
{
	var StartplatsKvar = false;
	
	// Kollar igenom om startplatsen värdet skall vara kvar
	for (var i = 0; i < globalStore.getCount(); i++)
	{
		// Om det nya uppdaterade värdet redan finns med skall den gamla tas bort. 
		if (globalStore.getAt(i).get('startplats') == Startplats)
		{
			StartplatsKvar = true;
			break;
		}
	}
	
	// Om inte startplatsen är kvar skall de tas bort från startplatsgridden. 
	if (!StartplatsKvar)
	{
	    // Söker igenom och tar bort startplatsen
		for (var i = 0; i < startGlobalStore.getCount(); i++)
		{
			if (startGlobalStore.getAt(i).get('startplats') == Startplats)
			{
				// Tar bort raden med den gamla värdet. 
				var record = startGlobalStore.getAt(i);
				startGlobalStore.remove(record);
				
				break;
			}
		}
	}
	
	RäknaOm();
}

// Uppdaterar värdet av en startplats. 
function UppdateraStartplats(GammaltVärde, NyttVärde)
{
    var GammaltVärdeKvar = false;
	var NyttVärdeRedanMed = false;
	
	// Kollar igenom om det gamla värdet skall vara kvar
	for (var i = 0; i < globalStore.getCount(); i++)
	{
		// Om det nya uppdaterade värdet redan finns med skall den gamla tas bort. 
		if (globalStore.getAt(i).get('startplats') == GammaltVärde)
		{
			GammaltVärdeKvar = true;
			break;
		}
	}
	
	// Kollar om det nya värdet redan finns med. 
	for (var i = 0; i < startGlobalStore.getCount(); i++)
	{
		// Om det nya uppdaterade värdet redan finns med skall den gamla tas bort. 
		if (startGlobalStore.getAt(i).get('startplats') == NyttVärde)
		{
			NyttVärdeRedanMed = true;
			break;
		}
	}
	
	// Om det nya värdet inte finns med skall det läggas till. 
	if (!NyttVärdeRedanMed)
	{
		// Lägger till det nya värdet. 
		LäggTillStartplats(NyttVärde);
	
		// Uppdatera det gamla värdet endast om det skall finnas kvar. 
		if (GammaltVärdeKvar)
		{
			// Söker igenom och byter ut värde för det gamla värdet.
			for (var i = 0; i < startGlobalStore.getCount(); i++)
			{
				if (startGlobalStore.getAt(i).get('startplats') == GammaltVärde)
				{
					// Skriver tillbaka de angivna värdena för areal och avrundade värdet för CAN. 
					var record = startGlobalStore.getAt(i);
					record.set('startplats', GammaltVärde);
				}
			}
		}
	
		// Söker igenom och byter ut värde för det nya värdet.
		for (var i = 0; i < startGlobalStore.getCount(); i++)
		{
			if (startGlobalStore.getAt(i).get('startplats') == NyttVärde)
			{
				// Skriver tillbaka de angivna värdena för areal och avrundade värdet för CAN. 
				var record = startGlobalStore.getAt(i);
				record.set('startplats', NyttVärde);
				
				// Tar bort ett från startplatsnummer, eftersom en rad försvinner. 
				startplatsNummer--;
			}
		}
	}
	// Om det gamlar värdet inte skall vara kvar, ta bort det. 
	if (!GammaltVärdeKvar)
	{
		// Söker igenom och tar bort startplatsen
		for (var i = 0; i < startGlobalStore.getCount(); i++)
		{
			if (startGlobalStore.getAt(i).get('startplats') == GammaltVärde)
			{
				// Tar bort raden med den gamla värdet. 
				var record = startGlobalStore.getAt(i);
				startGlobalStore.remove(record);
				
				break;
			}
		}
	}
}

// Räknar om siffrorna i startgridden. 
function StartRäknaOm()
{
	// Går igenom alla startplatser. 
	if (startGlobalStore != null)
	{
		for (var j = 0; j < startGlobalStore.getCount(); j++)
		{
			var areal = 0;
			var can = 0;
		
			// Går igenom alla fält i gridden för att kunna sumera per startplats. 
			for (var i = 0; i < globalStore.getCount(); i++)
			{
				// Om vi har hittat startplatsen i gridden över startplatser skall dess area och can läggas till. 
				if (globalStore.getAt(i).get('startplats') == startGlobalStore.getAt(j).get('startplats'))
				{
					// Summerar ihop areal och CAN. 
					areal += globalStore.getAt(i).get('areal') - 0; // parsar till ett tal. 
					can += globalStore.getAt(i).get('can') - 0; // parsar till ett tal. 
				}
			}
		
			// Sätter max en decimal på areal
			areal = areal.toFixed(1);
		
			// Skriver tillbaka de angivna värdena för areal och avrundade värdet för CAN. 
			var record = startGlobalStore.getAt(j);
			record.set('areal', areal);
			record.set('can', Math.round(can));
			//record.commit();
		}
	}
}

function StartKorrektIFylld()
{
	var felUpptäckt = false;
	var fel = "";
	
	if (globalStore != null)
	{
		for (var i = 0; i < startGlobalStore.getCount(); i++)
		{
			// Kontrollerar den nordliga koordinaten
			if (startGlobalStore.getAt(i).get('nkoord') == '')
			{
				if (felUpptäckt)
					fel += '\n';
				
				fel += "Nordlig koordinat (i Startplatser) på rad " + (i+1) + " är ej ifylld.";
				felUpptäckt = true;
			}
			
			// Kontrollerar den ostliga koordinaten
			if (startGlobalStore.getAt(i).get('okoord') == '')
			{
				if (felUpptäckt)
					fel += '\n';
				
				fel += "Ostlig koordinat (i Startplatser) på rad " + (i+1) + " är ej ifylld.";
				felUpptäckt = true;
			}
		}
	}
	
	return fel;
}

// Bygger upp en xml-del för alla startplatser. 
function StartHämtaXML()
{
	xml = '';
	
	if (startGlobalStore != null)
	{
		// Går igenom alla startplatser. 
		for (var j = 0; j < startGlobalStore.getCount(); j++)
		{
			xml = xml + "\n\t<Startplats>";
			
			xml += "\n\t\t<Startplats_startplats>" + FixaTillTecken(startGlobalStore.getAt(j).get('startplats')) + "</Startplats_startplats>";
			xml += "\n\t\t<Nordligkoordinat_startplats>" + startGlobalStore.getAt(j).get('nkoord') + "</Nordligkoordinat_startplats>";
			xml += "\n\t\t<Ostligkoordinat_startplats>" + startGlobalStore.getAt(j).get('okoord') + "</Ostligkoordinat_startplats>";
			xml += "\n\t\t<Areal_ha_startplats>" + startGlobalStore.getAt(j).get('areal') + "</Areal_ha_startplats>";
			xml += "\n\t\t<CAN_ton_startplats>" + startGlobalStore.getAt(j).get('can') + "</CAN_ton_startplats>";
			
			// Lägger till alla objekt kopplade till denna startplats. 
			xml += HämtaXML(startGlobalStore.getAt(j).get('startplats'));
			
			xml += "\n\t</Startplats>";
		}
		
		// Lägger till alla reservobjekt
		xml += RHämtaXML();
	}
	
	return xml;
}

// Bygger upp en text-del för alla startplatser
function StartHämtaText()
{
	text = 'Startplatser: ';
	
	if (startGlobalStore != null)
	{
		// Går igenom alla startplatser. 
		for (var j = 0; j < startGlobalStore.getCount(); j++)
		{
			if (j > 0)
				text += "\n";
		
			// Bygger upp texten för varje startplats
			text += "\nStartplats: " + startGlobalStore.getAt(j).get('startplats');
			text += "\nNordlig koordinat/startplats: " + startGlobalStore.getAt(j).get('nkoord');
			text += "\nOstlig koordinat/startplats: " + startGlobalStore.getAt(j).get('okoord');
			text += "\nAreal ha/startplats: " + startGlobalStore.getAt(j).get('areal');
			text += "\nCAN ton/startplats: " + startGlobalStore.getAt(j).get('can');
			
			// Lägger till alla objekt kopplade till denna startplats. 
			text += HämtaText(startGlobalStore.getAt(j).get('startplats'));
		}
		
		// Lägger till alla reservobjekt
		text += RHämtaText();
	}
	
	return text;
}

function UpdateCoordinates(type) {
	if(type === 0) {
		startGlobalCm.lookup[1].editor.field.minValue = 6130000;
		startGlobalCm.lookup[1].editor.field.maxValue = 7680000;
	
		startGlobalCm.lookup[2].editor.field.minValue = 1220000;
		startGlobalCm.lookup[2].editor.field.maxValue = 1880000;
	}
	else {
		startGlobalCm.lookup[1].editor.field.minValue = 6126000;
		startGlobalCm.lookup[1].editor.field.maxValue = 7693000;
	
		startGlobalCm.lookup[2].editor.field.minValue = 218000;
		startGlobalCm.lookup[2].editor.field.maxValue = 1084000;
	}
}
