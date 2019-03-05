<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Entreprenor"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($mysqli->connect_errno) {
    printf("Anslutningsfel: %s\n", $mysqli->connect_error);
    exit();
}

// Select
$sql="SELECT * FROM `$tbl_name`";
$result = $con->query($sql);
$row = $result->fetch_array(MYSQLI_ASSOC);

// To .NET
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";

while($row = $result->fetch_assoc())
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

$result->free();
$con->close();
?>
