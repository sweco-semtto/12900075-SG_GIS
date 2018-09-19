<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Foretag"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// Hämtar endast startplatser för senaste säsongen. 
$dateFormatYear = date('Y');
$date = date($dateFormatYear, time());
$date = $date -1;
$dateLimit = $date . "0901";

// Select
$sql="SELECT * FROM `$tbl_name` WHERE `Bestallningsdatum` > " . $dateLimit;
$result=mysql_query($sql);

// To .NET
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";

while($row = mysql_fetch_array($result))
{
	echo "<Foretag>";
	echo "\n";
	echo "<OrderID>" . $row["OrderID"] . "</OrderID>";	
	echo "\n";
	echo "<Ordernr>" . $row["Ordernr"] . "</Ordernr>";	
	echo "\n";
	echo "<Bestallningsreferens>" . $row["Bestallningsreferens"] . "</Bestallningsreferens>";	
	echo "\n";
	echo "<Bestallningsdatum>" . $row["Bestallningsdatum"] . "</Bestallningsdatum>";	
	echo "\n";
	echo "<Tidsstampel>" . $row["Tidsstampel"] . "</Tidsstampel>";	
	echo "\n";
	echo "<Foretagsnamn>" . $row["Foretagsnamn"] . "</Foretagsnamn>";	
	echo "\n";
	echo "<Faktureringsadress>" . $row["Faktureringsadress"] . "</Faktureringsadress>";	
	echo "\n";
	echo "<Postnummer>" . $row["Postnummer"] . "</Postnummer>";	
	echo "\n";
	echo "<Ort>" . $row["Ort"] . "</Ort>";
	echo "\n";	
	echo "<Region_Forvaltning>" . $row["Region_Forvaltning"] . "</Region_Forvaltning>";	
	echo "\n";
	echo "<Distrikt_Omrade>" . $row["Distrikt_Omrade"] . "</Distrikt_Omrade>";	
	echo "\n";
	echo "<Lan>" . $row["Lan"] . "</Lan>";	
	echo "\n";
	echo "<Kontaktperson1>" . $row["Kontaktperson1"] . "</Kontaktperson1>";	
	echo "\n";
	echo "<TelefonArb1>" . $row["TelefonArb1"] . "</TelefonArb1>";
	echo "\n";	
	echo "<TelefonMobil1>" . $row["TelefonMobil1"] . "</TelefonMobil1>";	
	echo "\n";
	echo "<TelefonHem1>" . $row["TelefonHem1"] . "</TelefonHem1>";	
	echo "\n";
	echo "<Epostadress1>" . $row["Epostadress1"] . "</Epostadress1>";	
	echo "\n";
	echo "<Kontaktperson2>" . $row["Kontaktperson2"] . "</Kontaktperson2>";	
	echo "\n";
	echo "<TelefonArb2>" . $row["TelefonArb2"] . "</TelefonArb2>";	
	echo "\n";
	echo "<TelefonMobil2>" . $row["TelefonMobil2"] . "</TelefonMobil2>";
	echo "\n";	
	echo "<TelefonHem2>" . $row["TelefonHem2"] . "</TelefonHem2>";	
	echo "\n";
	echo "<Epostadress2>" . $row["Epostadress2"] . "</Epostadress2>";	
	echo "\n";
	echo "<Kommentar>" . $row["Kommentar"] . "</Kommentar>";	
	echo "\n";
	echo "<OrdernrText>" . $row["OrdernrText"] . "</OrdernrText>";	
	echo "\n";
	echo "<Borttagen>" . "0" . "</Borttagen>";
	echo "\n";
	echo "</Foretag>";
	echo "\n";
}
echo "</MessageXML>";
mysql_close();
?>