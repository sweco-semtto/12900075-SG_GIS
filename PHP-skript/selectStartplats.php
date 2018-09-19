<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Startplats"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// Hämtar endast startplatser för senaste säsongen. 
$dateFormatYear = date('Y');
$date = date($dateFormatYear, time());
$date = $date -1;
$dateLimit = $date . "0901";

// Select
$sql="SELECT * FROM `$tbl_name` join SG_Foretag on SG_Startplats.OrderID = SG_Foretag.OrderID where SG_Foretag.Bestallningsdatum > " . $dateLimit;
$result=mysql_query($sql);

// To .NET
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";

while($row = mysql_fetch_array($result))
{
	echo "<Startplats>";
	echo "\n";
	echo "<ID>" . $row["ID"] . "</ID>";	
	echo "\n";
	echo "<ID_Access>" . $row["ID_Access"] . "</ID_Access>";
	echo "\n";
	echo "<OrderID>" . $row["OrderID"] . "</OrderID>";	
	echo "\n";
	echo "<Ordernr>" . $row["Ordernr"] . "</Ordernr>";	
	echo "\n";
	echo "<Startplats_startplats>" . $row["Startplats"] . "</Startplats_startplats>";	
	echo "\n";
	echo "<Nordligkoordinat_startplats>" . $row["Nordligkoordinat_startplats"] . "</Nordligkoordinat_startplats>";	
	echo "\n";
	echo "<Ostligkoordinat_startplats>" . $row["Ostligkoordinat_startplats"] . "</Ostligkoordinat_startplats>";	
	echo "\n";
	echo "<Areal_ha_startplats>" . $row["Areal_ha_startplats"] . "</Areal_ha_startplats>";	
	echo "\n";
	echo "<Skog_CAN_ton_startplats>" . $row["Skog_CAN_ton_startplats"] . "</Skog_CAN_ton_startplats>";	
	echo "\n";
	echo "<Ingaende_Objekt>" . $row["Ingaende_Objekt"] . "</Ingaende_Objekt>";	
	echo "\n";
	echo "<Kommentar1>" . $row["Kommentar1"] . "</Kommentar1>";	
	echo "\n";
	echo "<Kommentar2>" . $row["Kommentar2"] . "</Kommentar2>";	
	echo "\n";
	echo "<Kommentar3>" . $row["Kommentar3"] . "</Kommentar3>";	
	echo "\n";
	echo "<Kommentar4>" . $row["Kommentar4"] . "</Kommentar4>";	
	echo "\n";
	echo "<Status>" . $row["Status"] . "</Status>";
	echo "\n";
	echo "<Fraktentreprenors_ID>" . $row["Fraktentreprenors_ID"] . "</Fraktentreprenors_ID>";
	echo "\n";
	echo "<Spridningsentreprenors_ID>" . $row["Spridningsentreprenors_ID"] . "</Spridningsentreprenors_ID>";
	echo "\n";
	echo "<Bestallningsdatum>" . $row["Bestallningsdatum"] . "</Bestallningsdatum>";
	echo "\n";
	echo "<Borttagen>" . "0" . "</Borttagen>";
	echo "\n";
	echo "</Startplats>";
	echo "\n";
}
echo "</MessageXML>";
mysql_close();
?>