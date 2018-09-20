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
$sql="SELECT * FROM `SG_Entreprenor`";
$result=mysql_query($sql);

echo "<table>";
echo "<tr><td>Enreprenör</td><td width='10px'></td><td>Id</td>";
echo "<br>";
	
while($row = mysql_fetch_array($result))
{
	echo "<tr><td>" . $row["Namn"] . "</td><td width='10px'></td><td>" . $row["ID"] . "</tr>";
}

echo "</table>";

mysql_close();
?>
