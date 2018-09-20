<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Entreprenor"; // Table name 

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
	echo "<Entreprenor>";
	echo "\n";
	echo "<ID>" . $row["ID"] . "</ID>";
	echo "\n";
	echo "<Namn>" . $row["Namn"] . "</Namn>";
	echo "\n";
	echo "<Fraktentreprenor>" . $row["Fraktentreprenor"] . "</Fraktentreprenor>";
	echo "\n";
	echo "<Spridningsentreprenor>" . $row["Spridningsentreprenor"] . "</Spridningsentreprenor>";
	echo "\n";
	echo "<Anvandarnamn>" . $row["Anvandarnamn"] . "</Anvandarnamn>";
	echo "\n";
	echo "<Losenord>" . $row["Losenord"] . "</Losenord>";
	echo "\n";
	echo "</Entreprenor>";
	echo "\n";
}
echo "</MessageXML>";

mysql_close();
?>
