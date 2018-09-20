<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Reservobjekt"; // Table name 

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
		echo "<Reservobjekt>";
		echo "\n";
		echo "<OrderID>" . $row["OrderID"] . "</OrderID>";
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
		echo "<Kommentar>" . $row["Kommentar"] . "</Kommentar>";
		echo "\n";
		echo "</Reservobjekt>";
		echo "\n";
	}
echo "</MessageXML>";

mysql_close();
?>
