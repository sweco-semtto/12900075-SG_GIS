<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Foretag"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// Data sent from form .NET
$Ordernr=$_POST['Ordernr']; 
$Bestallningsreferens=$_POST['Bestallningsreferens']; 
$Bestallningsdatum=$_POST['Bestallningsdatum']; 
$Tidsstampel=$_POST['Tidsstampel']; 
$Foretagsnamn=$_POST['Foretagsnamn']; 
$Faktureringsadress=$_POST['Faktureringsadress']; 
$Postnummer=$_POST['Postnummer']; 
$Ort=$_POST['Ort']; 
$Region_Forvaltning=$_POST['Region_Forvaltning']; 
$Distrikt_Omrade=$_POST['Distrikt_Omrade']; 
$VAT=$_POST['VAT']; 
$Kontaktperson1=$_POST['Kontaktperson1']; 
$TelefonArb1=$_POST['TelefonArb1']; 
$TelefonMobil1=$_POST['TelefonMobil1']; 
$TelefonHem1=$_POST['TelefonHem1']; 
$Epostadress1=$_POST['Epostadress1']; 
$Kontaktperson2=$_POST['Kontaktperson2']; 
$TelefonArb2=$_POST['TelefonArb2']; 
$TelefonMobil2=$_POST['TelefonMobil2']; 
$TelefonHem2=$_POST['TelefonHem2']; 
$Epostadress2=$_POST['Epostadress2']; 
$Kommentar=$_POST['Kommentar']; 
$OrdernrText=$_POST['OrdernrText'];

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES 
('$OrderID', '$Ordernr', '$Bestallningsreferens', '$Bestallningsdatum',
'$Tidsstampel', '$Foretagsnamn', '$Faktureringsadress', '$Postnummer',
'$Ort', '$Region_Forvaltning', '$Distrikt_Omrade', '$VAT', '$Kontaktperson1',
'$TelefonArb1', '$TelefonMobil1', '$TelefonHem1', '$Epostadress1', '$Kontaktperson2',
'$TelefonArb2', '$TelefonMobil2', '$TelefonHem2', '$Epostadress2', '$Kommentar',
'$OrdernrText')";
$result=mysql_query($sql);
$affectedRows = mysql_affected_rows();

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
if ($affectedRows == 1)
{
	echo "Success, inserted " . "'$Ordernr'";
}
else
{
	echo "Failure, not inserted " . "'$Ordernr'";
	echo "\n";
	echo $sql;
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>