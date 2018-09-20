<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test_Status"; // Table name 

// Connect to server and select database.
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
	echo "<Status>";
	echo "\n";
	echo "<ID>" . $row["ID"] . "</ID>";	
	echo "\n";
	echo "<StartplatsID>" . $row["StartplatsID"] . "</StartplatsID>";
	echo "\n";
	echo "<Datum>" . $row["Datum"] . "</Datum>";	
	echo "\n";
	echo "<AndradAv>" . $row["AndradAv"] . "</AndradAv>";	
	echo "\n";
	echo "<Status_status>" . $row["Status_status"] . "</Status_status>";	
	echo "\n";
	echo "<Borttagen>" . $row["Borttagen"] . "</Borttagen>";
	echo "\n";
	echo "</Status>";
	echo "\n";
}
echo "</MessageXML>";
mysql_close();
?>