<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Objekt"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// Select
$sql="SELECT * FROM `$tbl_name`";
$result=mysql_query($sql);

// To .NET
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
	while($row = mysql_fetch_array($result))
	{
		echo "<Objekt>";
		echo "\n";
		echo "<OrderID>" . $row["OrderID"] . "</OrderID>";
		echo "\n";
		echo "<Startplats>" . $row["Startplats"] . "</Startplats>";
		echo "\n";
		echo "<Objektnr>" . $row["Objektnr"] . "</Objektnr>";
		echo "\n";
		echo "<Avdnr>" . $row["Avdnr"] . "</Avdnr>";
		echo "\n";
		echo "<Avdnamn>" . $row["Avdnamn"] . "</Avdnamn>";
		echo "\n";
		echo "<Areal_ha>" . $row["Areal_ha"] . "</Areal_ha>";
		echo "\n";
		echo "<Giva_KgN_ha>" . $row["Giva_KgN_ha"] . "</Giva_KgN_ha>";
		echo "\n";
		echo "<Skog_CAN_ton>" . $row["Skog_CAN_ton"] . "</Skog_CAN_ton>";
		echo "\n";
		echo "<Kommentar>" . $row["Kommentar"] . "</Kommentar>";
		echo "\n";
		echo "</Objekt>";
		echo "\n";
	}
echo "</MessageXML>";

mysql_close();
?>
