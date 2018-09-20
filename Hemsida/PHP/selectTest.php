<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test"; // Table name 

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
echo "<Data>";
echo "\n";

	$count=mysql_num_rows($result);
	echo "<NoOfRows>";
	echo "\n";
	echo "Antal: " . "'$count'";
	echo "\n";
	echo "</NoOfRows>";
	echo "\n";

	while($row = mysql_fetch_array($result))
	{
		echo "<Row>";
		echo "\n";
		echo "ID: " . $row["ID"] . "    Text: " . $row["Text"];
		echo "\n";
		echo "</Row>";
		echo "\n";
	}

echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>
