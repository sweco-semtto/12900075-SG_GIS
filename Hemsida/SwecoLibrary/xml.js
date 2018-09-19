	// Global varibel som innehåller formuläret. 
	var thisForm;
		
		// Hämtar tiden från servern och bygger sedan upp xml:en. 
		function ByggUppXML(thisForm)
		{
			// Sparar undan formuläret. 
			this.thisForm = thisForm;
			
			// Kontrollerar formuläret
			if (!KontrolleraFormular(this.thisForm))
				return;
			
			// Hämtar tiden från servern m.h.a. php 
			Ext.Ajax.request(
			{
				url: 'http://www.sg-systemet.com/bestallning/Gettime.php',
				success : function (resultSuccess, requestFailure) { SkapaOrdernummer(resultSuccess.responseText); },
				failure : function (resultSuccess, requestFailure) { DatumEjFunnet(); },
				params: {}
			});
		}
		
		/**
		 * Tar ut ett datum från en tidsstämpel. Alla beställningar från 1:a oktober eller senare kommer med på nästa års beställningar. 
		 * @param {String} timestamp - Består av Datum, klockslag och millisekunder enligt formen ÅÅÅÅ-MM-DD TT:MM:SS milsek (t.ex. 2017-01-11 07:06:33 182000)
		 * @return {String} - Returnerar ett år enligt formen ÅÅÅÅ (t.ex. 2017)
		 */
		function GetYearFromTimeStamp(timestamp)
		{
			var ans = timestamp.slice(0, 4);
			var month = timestamp.slice(6, 7);
			if (parseInt(month) >= 10) // Brytdatum är 1:a oktober så därför lägger vi på +1 på året. 
				ans = (parseInt(ans)+1).toString();
			return ans;
		}
		
		/**
		 * Tar ut ett datum från en tidsstämpel. 
		 * @param {String} timestamp - Består av Datum, klockslag och millisekunder enligt formen ÅÅÅÅ-MM-DD TT:MM:SS milsek (t.ex. 2017-01-11 07:06:33 182000)
		 * @return {String} - Returnerar ett datum enligt formen ÅÅÅÅ-MM-DD (t.ex. 2017-01-11)
		 */
		function GetDateFromTimeStamp(timestamp)
		{
			var ans = timestamp.slice(0,10);
			return ans;
		}
		
		/**
		 * Skapar ett ordernummer m.h.a. tiden och skickar tillbaka detta. 
		 * @param {String} timestamp - Består av Datum, klockslag och millisekunder enligt formen ÅÅÅÅ-MM-DD TT:MM:SS milsek (t.ex. 2017-01-11 07:06:33 182000)
		 * @return {String} - Returnerar ett ordernummer, om ingen order kunde skapas returnerars ordernumret 0. 
		 */
		function SkapaOrdernummer(timestamp)
		{
			var year = GetYearFromTimeStamp(timestamp);
			var date = GetDateFromTimeStamp(timestamp)
		
			// Hämtar skapar ett ordernummer m.h.a. tiden.  
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
		 * @param {String} datum - Datumet när orderen skapades. 
		 * @param {String} ordernummerr - Ordernumret för ordern. 
		 */
		function ByggUppXMLMedTid(ordernummer, datum, timestamp)
		{	
			var xml = "";

			xml += "<Best&aumlllning>";
			xml += "\n\t<F&oumlretag>";
			
			// Lägger till ordernummer och dagens datum
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
			
			xml += StartHämtaXML();
			
			xml += "\n\t<Kommentar>" + FixaTillTecken(formular.upplysningar.value) + "</Kommentar>";

			xml += "\n</Best&aumlllning>";
						
			// Hämtar den lättlästa texten. 
			var text = ByggUppText(thisForm, ordernummer, datum);
			text = FixaTillTecken(text);
			
			// Skickar iväg xmlen till php som mellanlagrar ordern i databasen som i sin tur skickar iväg ett mail.
			BackupOrder(ordernummer, timestamp, xml, text, formular);
		}
		
		/**
		 * Tar en säkerhetskopia på ordern om mejlet inte skulle komma fram. 
		 * @params {string} time - Tiden från servern. 
		 * @params {string} xml - Xml:en som skickas via mejl till Skogens Gödsling. 
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
		 * Skickar epost till Skogens Gödsling och till kunden som beställt. 
		 */
		function SkickaEpost(ordernummer, xml, text, formular)
		{
			// Skickar iväg xmlen till php-sidan som i sin tur skickar iväg ett mail.
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
		 * @params {string} thisForm - Fomuläret som användaren har fyllt i. 
		 * @parmas {string} ordernummer - Ordernumret. 
		 * @parmas {string} datum - Beställningsdatumet. 
		 */
		function ByggUppText(thisForm, ordernummer, datum)
		{
			// Bygger upp beställningstexten. 
			var text = "";
			text += "Ordernummer: " + ordernummer;
			text += "\nBeställningsdatum: " + datum;
			
			// Lägger till Företagsnamn m.m.
			text += "\nFöretagsnamn: " + formular.Foretag.value;
			text += "\nFaktureringsadress: " + formular.FAdress.value;
			text += "\nPostnummer: " + formular.FPostnummer.value;
			text += "\nOrt: " + formular.FOrt.value;
			text += "\nRegion/Förvaltning: " + formular.Region.value;
			text += "\nVAT: " + formular.VAT.value;

			text += "\nDistrikt/Område: " + formular.Distrikt.value;
			text += "\nBeställningsreferens: " + formular.Bestallningsreferens.value;
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
			
			text += "\n\n" + StartHämtaText();
			text += "\n\nKommentar: " + formular.upplysningar.value;
			
			return text;
		}
		
		// Om tiden inte går att hämta ifrån servern. 
		function DatumEjFunnet(requestFailure)
		{
			alert('Kunde inte hämta datum. Vänligen gör ett nytt försök eller hör av dig till Skogens Gödsling. ');
		}
		
		/**
		 * Funktion som meddelar användaren att en backup inte är gjord. 
		 */
		function BackupNotDone(requestFailure)
		{
			Alert("Kan inte spara din beställning, vänligen kontakta Skogens Gödsling");
		}
		
		// Om beställningse-posten har skickats till SG så skall kontaktpersonerna också få ett mail. 
		function EpostSkickat(resultSuccess)
		{
			alert('Beställningen har skickats till Skogens Gödsling. Du får snart ett mail som visar dina beställningsuppgifter.');
		}
		
		// Om beställningse-posten inte har skickats till SG så skall detta meddelas till användaren. 
		function EpostInteSkickat(requestFailure)
		{
			alert('Beställningen har inte kommit fram till Skogens Gödsling. Vänligen gör ett nytt försök eller hör av dig till Skogens Gödsling. ');
		}
		
		// Byter ut alla å, ä och ö till html-tecken så att tecken inte visas fel i vissa e-postprogram. 
		function FixaTillTecken(text)
		{
			var ans = "";
			
			if (text == null)
				return null;
		
			// Ersätter varje å, ä och ö med html-motsvarigheten. 
			for (var i = 0; i < text.length; i++)
			{
				var bokstav = text.charAt(i);
				
				if (bokstav == 'å')
				{
					ans += "&aring";
				}
				else if (bokstav == 'Å')
				{
					ans += "&Aring";
				}
				else if (bokstav == 'ä')
				{
					ans += "&auml";
				}
				else if (bokstav == 'Ä')
				{
					ans += "&Auml";
				}
				else if (bokstav == 'ö')
				{
					ans += "&ouml";
				}
				else if (bokstav == 'Ö')
				{
					ans += "&Ouml";
				}
				else if (bokstav == 'ü')
				{
					ans += "&uuml";
				}
				else if (bokstav == 'Ü')
				{
					ans += "&Uuml";
				}
				else if (bokstav == 'û')
				{
					ans += "&ucirc";
				}
				else if (bokstav == 'Û')
				{
					ans += "&Ucirc";
				}
				else if (bokstav == 'é')
				{
					ans += "&egrave";
				}
				else if (bokstav == 'É')
				{
					ans += "&Egrave";
				}
				else if (bokstav == 'è')
				{
					ans += "&eacute";
				}
				else if (bokstav == 'È')
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
		
		// Kontrollerar så att alla uppgifter som måste vara ifyllda är ifyllda. 
		function KontrolleraFormular(thisForm)
		{
			var FormularOk = true;
			var Felmeddelande = "";
		
			// Kontrollerar Företag
			if (formular.Foretag.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i företag. ';
				FormularOk = false;
			}
			
			// Kontrollerar Postnummer
			if (formular.FPostnummer.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i företagets postnummer. ';
				FormularOk = false;
			}
			
			// Kontrollerar Ort
			if (formular.FOrt.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i företagets ort. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 namn
			if (formular.Namn1.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i kontaktperson 1:s namn. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 telefon arbetet
			if (formular.TfnArb1.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i kontaktperson 1:s telefon till arbetet. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 telefon mobil
			if (formular.TfnMob1.value == '')
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i kontaktperson 1:s mobilnummer. ';
				FormularOk = false;
			}
			
			// Kontrollerar Kontaktperson1 epost
			if (!InnehållerTecken(formular.Epost1.value, '@.'))
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste fylla i kontaktperson 1:s e-postadress. ';
				FormularOk = false;
			}
			
			// Kontrollerar att ett koordinatsystem är valt. 
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
				Felmeddelande += 'Måste fylla i minst ett koordinatystem, antingen RT90 eller SWEREF99. ';
				FormularOk = false;
			}
			
			// Kontrollerar så att alla obligatoriska celler i översta gridden är ifyllda.
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
			
			// Kontrollerar så att alla obligatoriska celler i mellersta gridden är ifyllda.
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
			
			// Kontrollerar så att alla obligatoriska celler i nedersta gridden är ifyllda.
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
			
			// Kontrollerar så att kryssrutan är ikryssad
			if (!formular.skickaKartfiler.checked)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				Felmeddelande += 'Måste kryssa i rutan om inskickande av kartfiler. ';
				FormularOk = false;
			}
			
			// kollar att inga kommentarer är längre än 30 tecken. 
			var errors = KollaKommentarer();
			if (errors.length > 0)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				
				for (var i = 0; i < errors.length; i++)
				{
					Felmeddelande += 'Kommentaren för rad ' + errors[i] + ' för gridden "Objekt att gödsla" är längre än 30 tecken. ';
				}
				FormularOk = false;
			}
			
			// Kollar att inga kommentarer är längre än 30 tecken. 
			var Rerrors = RKollaKommentarer();
			if (Rerrors.length > 0)
			{
				if (!FormularOk)
				{
					Felmeddelande += '\n';
				}
				
				for (var i = 0; i < Rerrors.length; i++)
				{
					Felmeddelande += 'Kommentaren för rad ' + Rerrors[i] + ' för gridden "Reservobjekt att eventuellt gödsla" är längre än 30 tecken. "';
				}
				FormularOk = false;
			}
			
			// Om några fel hittat skall de rapporteras, d.v.s skrivas ut på skärmen i en ruta. 
			if (!FormularOk)
			{
				alert("Följande fel finns i formuläret: \n" + Felmeddelande);
			}
			
			return FormularOk;
		}

		// Kontrollerar om texten innehåller de tecken som är med. Om någon sträng är tom returneras false. 
		function InnehållerTecken(Text, Tecken)
		{
			var innehållerTecken = true;
			
			// Kollar basfall
			if (Text.length == 0)
				return false;
			if (Tecken.length == 0)
				return false;
			
			// Går igenom alla tecken och kollar att de finns med itexten
			for (var teckenNummer = 0; teckenNummer < Tecken.length && innehållerTecken; teckenNummer++)
			{
				innehållerTecken = false;
				for (var textTeckenNummer = 0; textTeckenNummer < Text.length; textTeckenNummer++)
				{
					if (Text.charAt(textTeckenNummer) == Tecken.charAt(teckenNummer))
					{
						innehållerTecken = true;
						break;
					}
				}
			}
			
			return innehållerTecken;
		}