	// Global varibel som inneh�ller formul�ret. 
	var thisForm;
		
		// H�mtar tiden fr�n servern och bygger sedan upp xml:en. 
		function ByggUppXML(thisForm)
		{
			// Sparar undan formul�ret. 
			this.thisForm = thisForm;
			
			// Kontrollerar formul�ret
			if (!KontrolleraFormular(this.thisForm))
				return;
			
			// H�mtar tiden fr�n servern m.h.a. php 
			Ext.Ajax.request(
			{
				url: 'http://www.sg-systemet.com/bestallning/Gettime.php',
				success : function (resultSuccess, requestFailure) { SkapaOrdernummer(resultSuccess.responseText); },
				failure : function (resultSuccess, requestFailure) { DatumEjFunnet(); },
				params: {}
			});
		}
		
		/**
		 * Tar ut ett datum fr�n en tidsst�mpel. Alla best�llningar fr�n 1:a oktober eller senare kommer med p� n�sta �rs best�llningar. 
		 * @param {String} timestamp - Best�r av Datum, klockslag och millisekunder enligt formen ����-MM-DD TT:MM:SS milsek (t.ex. 2017-01-11 07:06:33 182000)
		 * @return {String} - Returnerar ett �r enligt formen ���� (t.ex. 2017)
		 */
		function GetYearFromTimeStamp(timestamp)
		{
			var ans = timestamp.slice(0, 4);
			var month = timestamp.slice(6, 7);
			if (parseInt(month) >= 10) // Brytdatum �r 1:a oktober s� d�rf�r l�gger vi p� +1 p� �ret. 
				ans = (parseInt(ans)+1).toString();
			return ans;
		}
		
		/**
		 * Tar ut ett datum fr�n en tidsst�mpel. 
		 * @param {String} timestamp - Best�r av Datum, klockslag och millisekunder enligt formen ����-MM-DD TT:MM:SS milsek (t.ex. 2017-01-11 07:06:33 182000)
		 * @return {String} - Returnerar ett datum enligt formen ����-MM-DD (t.ex. 2017-01-11)
		 */
		function GetDateFromTimeStamp(timestamp)
		{
			var ans = timestamp.slice(0,10);
			return ans;
		}
		
		/**
		 * Skapar ett ordernummer m.h.a. tiden och skickar tillbaka detta. 
		 * @param {String} timestamp - Best�r av Datum, klockslag och millisekunder enligt formen ����-MM-DD TT:MM:SS milsek (t.ex. 2017-01-11 07:06:33 182000)
		 * @return {String} - Returnerar ett ordernummer, om ingen order kunde skapas returnerars ordernumret 0. 
		 */
		function SkapaOrdernummer(timestamp)
		{
			var year = GetYearFromTimeStamp(timestamp);
			var date = GetDateFromTimeStamp(timestamp)
		
			// H�mtar skapar ett ordernummer m.h.a. tiden.  
			Ext.Ajax.request(
			{
				url: 'http://www.sg-systemet.com/bestallning/CreateOrdernumber.php',
				success : function (resultSuccess, requestFailure) { ByggUppXMLMedTid(resultSuccess.responseText, date, timestamp); },
				failure : function (resultSuccess, requestFailure) { DatumEjFunnet(); },
				params: {Year:year, Timestamp:timestamp}
			});
		}
		
		/**
		 * Kontrollerar och bygger sedan upp xml-en som skickas via mail sist i denna funktion.
		 * @param {String} datum - Datumet n�r orderen skapades. 
		 * @param {String} ordernummerr - Ordernumret f�r ordern. 
		 */
		function ByggUppXMLMedTid(ordernummer, datum, timestamp)
		{	
			var xml = "";

			xml += "<Best&aumlllning>";
			xml += "\n\t<F&oumlretag>";
			
			// L�gger till ordernummer och dagens datum
			xml += "\n\t\t<Ordernummer>" + ordernummer + "</Ordernummer>";
			xml += "\n\t\t<Best&aumlllningsdatum>" + datum + "</Best&aumlllningsdatum>";

			xml += "\n\t\t<F&oumlretagsnamn>" + FixaTillTecken(formular.Foretag.value) + "</F&oumlretagsnamn>";
			xml += "\n\t\t<Faktureringsadress>" + FixaTillTecken(formular.FAdress.value) + "</Faktureringsadress>";
			xml += "\n\t\t<Postnummer>" + FixaTillTecken(formular.FPostnummer.value) + "</Postnummer>";
			xml += "\n\t\t<Ort>" + FixaTillTecken(formular.FOrt.value) + "</Ort>";
			xml += "\n\t\t<Region_F&oumlrvaltning>" + FixaTillTecken(formular.Region.value) + "</Region_F&oumlrvaltning>";
			xml += "\n\t\t<VAT>" + "SE" + FixaTillTecken(formular.VAT.value).replace('-','') + "01" + "</VAT>";
			xml += "\n\t\t<Distrikt_Omr&aringde>" + FixaTillTecken(formular.Distrikt.value) + "</Distrikt_Omr&aringde>";
			xml += "\n\t\t<Best&aumlllningsreferens>" + FixaTillTecken(formular.Bestallningsreferens.value) + "</Best&aumlllningsreferens>";
			
			xml += "\n\t\t<Kontaktperson1>" + FixaTillTecken(formular.Namn1.value) + "</Kontaktperson1>";
			xml += "\n\t\t<Telefon_Arb1>" + FixaTillTecken(formular.TfnArb1.value) + "</Telefon_Arb1>";
			xml += "\n\t\t<Telefon_Mob1>" + FixaTillTecken(formular.TfnMob1.value) + "</Telefon_Mob1>";
			xml += "\n\t\t<Telefon_Hem1>" + FixaTillTecken(formular.TfnHem1.value) + "</Telefon_Hem1>";
			xml += "\n\t\t<E-postadress1>" + FixaTillTecken(formular.Epost1.value) + "</E-postadress1>";
			
			xml += "\n\t\t<Kontaktperson2>" + FixaTillTecken(formular.Namn2.value) + "</Kontaktperson2>";
			xml += "\n\t\t<Telefon_Arb2>" + FixaTillTecken(formular.TfnArb2.value) + "</Telefon_Arb2>";
			xml += "\n\t\t<Telefon_Mob2>" + FixaTillTecken(formular.TfnMob2.value) + "</Telefon_Mob2>";
			xml += "\n\t\t<Telefon_Hem2>" + FixaTillTecken(formular.TfnHem2.value) + "</Telefon_Hem2>";
			xml += "\n\t\t<E-postadress2>" + FixaTillTecken(formular.Epost2.value) + "</E-postadress2>";
			xml += "\n\t</F&oumlretag>";	
			if (formular.Koord[0].checked)
				xml += "\n\t\<Koordinatsystem>" + "RT90" + "</Koordinatsystem>";
			else
				xml += "\n\t\<Koordinatsystem>" + "SWEREF99" + "</Koordinatsystem>";
			
			xml += StartH�mtaXML();
			
			xml += "\n\t<Kommentar>" + FixaTillTecken(formular.upplysningar.value) + "</Kommentar>";

			xml += "\n</Best&aumlllning>";
						
			// H�mtar den l�ttl�sta texten. 
			var text = ByggUppText(thisForm, ordernummer, datum);
			text = FixaTillTecken(text);
			
			// Skickar iv�g xmlen till php som mellanlagrar ordern i databasen som i sin tur skickar iv�g ett mail.
			BackupOrder(ordernummer, timestamp, xml, text, formular);
		}
		
		/**
		 * Tar en s�kerhetskopia p� ordern om mejlet inte skulle komma fram. 
		 * @params {string} time - Tiden fr�n servern. 
		 * @params {string} xml - Xml:en som skickas via mejl till Skogens G�dsling. 
		 */
		function BackupOrder(ordernummer, time, xml, text, formular)
		{
			Ext.Ajax.request(
			{
				url: 'http://www.sg-systemet.com/bestallning/BackupOrder.php',
				success : function (resultSuccess, requestFailure) { SkickaEpost(ordernummer, xml, text, formular); },
				failure : function (resultSuccess, requestFailure) { BackupNotDone(requestFailure); },
				params: {time : time, ordernummer : ordernummer, xml : xml}
			});
		}
		
		/**
		 * Skickar epost till Skogens G�dsling och till kunden som best�llt. 
		 */
		function SkickaEpost(ordernummer, xml, text, formular)
		{
			// Skickar iv�g xmlen till php-sidan som i sin tur skickar iv�g ett mail.
			Ext.Ajax.request(
			{
				url: 'http://www.sg-systemet.com/bestallning/Bestallningsformular.php',
				success : function (resultSuccess, requestFailure) { EpostSkickat(resultSuccess); },
				failure : function (resultSuccess, requestFailure) { EpostInteSkickat(requestFailure); },
				params: {ordernummer : ordernummer, xml : xml, text : text, ton : formular.summaCAN.value, foretag : FixaTillTecken(formular.Foretag.value), kontakt1 : FixaTillTecken(formular.Epost1.value)}
			});
		}
		
		/**
		 * Bygger upp texten som skal skickas till kunden via mejl. 
		 * @params {string} thisForm - Fomul�ret som anv�ndaren har fyllt i. 
		 * @parmas {string} ordernummer - Ordernumret. 
		 * @parmas {string} datum - Best�llningsdatumet. 
		 */
		function ByggUppText(thisForm, ordernummer, datum)
		{
			// Bygger upp best�llningstexten. 
			var text = "";
			text += "Ordernummer: " + ordernummer;
			text += "\nBest�llningsdatum: " + datum;
			
			// L�gger till F�retagsnamn m.m.
			text += "\nF�retagsnamn: " + formular.Foretag.value;
			text += "\nFaktureringsadress: " + formular.FAdress.value;
			text += "\nPostnummer: " + formular.FPostnummer.value;
			text += "\nOrt: " + formular.FOrt.value;
			text += "\nRegion/F�rvaltning: " + formular.Region.value;
			text += "\nVAT: " + formular.VAT.value;

			text += "\nDistrikt/Omr�de: " + formular.Distrikt.value;
			text += "\nBest�llningsreferens: " + formular.Bestallningsreferens.value;
			text += "\nKontaktperson1: " + formular.Namn1.value;
			text += "\nTelefon Arb1: " + formular.TfnArb1.value;
			text += "\nTelefon Mob1: " + formular.TfnMob1.value;
			text += "\nTelefon Hem1: " + formular.TfnHem1.value;
			text += "\nE-postadress1: " + formular.Epost1.value;
			text += "\nKontaktperson2: " + formular.Namn2.value;
			text += "\nTelefon Arb2: " + formular.TfnArb2.value;
			text += "\nTelefon Mob2: " + formular.TfnMob2.value;
			text += "\nTelefon Hem2: " + formular.TfnHem2.value;
			text += "\nE-postadress2: " + formular.Epost2.value;
			
			text += "\n\n" + StartH�mtaText();
			text += "\n\nKommentar: " + formular.upplysningar.value;
			
			return text;
		}
		
		// Om tiden inte g�r att h�mta ifr�n servern. 
		function DatumEjFunnet(requestFailure)
		{
			alert('Kunde inte h�mta datum. V�nligen g�r ett nytt f�rs�k eller h�r av dig till Skogens G�dsling. ');
		}
		
		/**
		 * Funktion som meddelar anv�ndaren att en backup inte �r gjord. 
		 */
		function BackupNotDone(requestFailure)
		{
			Alert("Kan inte spara din best�llning, v�nligen kontakta Skogens G�dsling");
		}
		
		// Om best�llningse-posten har skickats till SG s� skall kontaktpersonerna ocks� f� ett mail. 
		function EpostSkickat(resultSuccess)
		{
			alert('Best�llningen har skickats till Skogens G�dsling. Du f�r snart ett mail som visar dina best�llningsuppgifter.');
		}
		
		// Om best�llningse-posten inte har skickats till SG s� skall detta meddelas till anv�ndaren. 
		function EpostInteSkickat(requestFailure)
		{
			alert('Best�llningen har inte kommit fram till Skogens G�dsling. V�nligen g�r ett nytt f�rs�k eller h�r av dig till Skogens G�dsling. ');
		}
		
		// Byter ut alla �, � och � till html-tecken s� att tecken inte visas fel i vissa e-postprogram. 
		function FixaTillTecken(text)
		{
			var ans = "";
			
			if (text == null)
				return null;
		
			// Ers�tter varje �, � och � med html-motsvarigheten. 
			for (var i = 0; i < text.length; i++)
			{
				var bokstav = text.charAt(i);
				
				if (bokstav == '�')
				{
					ans += "&aring";
				}
				else if (bokstav == '�')
				{
					ans += "&Aring";
				}
				else if (bokstav == '�')
				{
					ans += "&auml";
				}
				else if (bokstav == '�')
				{
					ans += "&Auml";
				}
				else if (bokstav == '�')
				{
					ans += "&ouml";
				}
				else if (bokstav == '�')
				{
					ans += "&Ouml";
				}
				else if (bokstav == '�')
				{
					ans += "&uuml";
				}
				else if (bokstav == '�')
				{
					ans += "&Uuml";
				}
				else if (bokstav == '�')
				{
					ans += "&ucirc";
				}
				else if (bokstav == '�')
				{
					ans += "&Ucirc";
				}
				else if (bokstav == '�')
				{
					ans += "&egrave";
				}
				else if (bokstav == '�')
				{
					ans += "&Egrave";
				}
				else if (bokstav == '�')
				{
					ans += "&eacute";
				}
				else if (bokstav == '�')
				{
					ans += "&Eacute";
				}
				else if (bokstav == '&')
				{
					ans += "&amp";
				}
				else if (bokstav == '<')
				{
					ans += "&lt";
				}
				else if (bokstav == '>')
				{
					ans += "&gt";
				}
				else if (bokstav == '"')
				{
					ans += "&quot";
				}
				else if (bokstav == "'")
				{
					ans += "&#39";
				}
				else
				{
					ans += bokstav;
				}
			}
			
			return ans;
		}
		
		// Kontrollerar s� att alla uppgifter som m�ste vara ifyllda �r ifyllda. 
		function KontrolleraFormular(thisForm)
		{
			var FormularOk = true;
			var Felmeddelande = "";
		
			// Kontrollerar F�retag
			if (formular.Foretag.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i f�retag. ';
				FormularOk = false;
			}
			
			// Kontrollerar Postnummer
			if (formular.FPostnummer.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i f�retagets postnummer. ';
				FormularOk = false;
			}
			
			// Kontrollerar Ort
			if (formular.FOrt.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i f�retagets ort. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 namn
			if (formular.Namn1.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i kontaktperson 1:s namn. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 telefon arbetet
			if (formular.TfnArb1.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i kontaktperson 1:s telefon till arbetet. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 telefon mobil
			if (formular.TfnMob1.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i kontaktperson 1:s mobilnummer. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 epost
			if (!Inneh�llerTecken(formular.Epost1.value, '@.'))
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i kontaktperson 1:s e-postadress. ';
				FormularOk = false;
			}
			
			// Kontrollerar att ett koordinatsystem �r valt. 
			var uncheckedRadioButtons = 0;
			for (var i = 0; i < formular.Koord.length; i++)
			{
				if (!formular.Koord[i].checked)
				{
					uncheckedRadioButtons++;
				}
			}
			if (uncheckedRadioButtons > 1)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste fylla i minst ett koordinatystem, antingen RT90 eller SWEREF99. ';
				FormularOk = false;
			}
			
			// Kontrollerar s� att alla obligatoriska celler i �versta gridden �r ifyllda.
			var tmpFelmeddelande = KorrektIFylld();
			if (tmpFelmeddelande != "")
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += tmpFelmeddelande;
				FormularOk = false;
			}
			
			// Kontrollerar s� att alla obligatoriska celler i mellersta gridden �r ifyllda.
			tmpFelmeddelande = StartKorrektIFylld();
			if (tmpFelmeddelande != "")
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += tmpFelmeddelande;
				FormularOk = false;
			}
			
			// Kontrollerar s� att alla obligatoriska celler i nedersta gridden �r ifyllda.
			tmpFelmeddelande = RKorrektIFylld();
			if (tmpFelmeddelande != "")
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += tmpFelmeddelande;
				FormularOk = false;
			}
			
			// Kontrollerar s� att kryssrutan �r ikryssad
			if (!formular.skickaKartfiler.checked)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'M�ste kryssa i rutan om inskickande av kartfiler. ';
				FormularOk = false;
			}
			
			// kollar att inga kommentarer �r l�ngre �n 30 tecken. 
			var errors = KollaKommentarer();
			if (errors.length > 0)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				
				for (var i = 0; i < errors.length; i++)
				{
					Felmeddelande += 'Kommentaren f�r rad ' + errors[i] + ' f�r gridden "Objekt att g�dsla" �r l�ngre �n 30 tecken. ';
				}
				FormularOk = false;
			}
			
			// Kollar att inga kommentarer �r l�ngre �n 30 tecken. 
			var Rerrors = RKollaKommentarer();
			if (Rerrors.length > 0)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				
				for (var i = 0; i < Rerrors.length; i++)
				{
					Felmeddelande += 'Kommentaren f�r rad ' + Rerrors[i] + ' f�r gridden "Reservobjekt att eventuellt g�dsla" �r l�ngre �n 30 tecken. "';
				}
				FormularOk = false;
			}
			
			// Om n�gra fel hittat skall de rapporteras, d.v.s skrivas ut p� sk�rmen i en ruta. 
			if (!FormularOk)
			{
				alert("F�ljande fel finns i formul�ret: \n" + Felmeddelande);
			}
			
			return FormularOk;
		}

		// Kontrollerar om texten inneh�ller de tecken som �r med. Om n�gon str�ng �r tom returneras false. 
		function Inneh�llerTecken(Text, Tecken)
		{
			var inneh�llerTecken = true;
			
			// Kollar basfall
			if (Text.length == 0)
				return false;
			if (Tecken.length == 0)
				return false;
			
			// G�r igenom alla tecken och kollar att de finns med itexten
			for (var teckenNummer = 0; teckenNummer < Tecken.length && inneh�llerTecken; teckenNummer++)
			{
				inneh�llerTecken = false;
				for (var textTeckenNummer = 0; textTeckenNummer < Text.length; textTeckenNummer++)
				{
					if (Text.charAt(textTeckenNummer) == Tecken.charAt(teckenNummer))
					{
						inneh�llerTecken = true;
						break;
					}
				}
			}
			
			return inneh�llerTecken;
		}