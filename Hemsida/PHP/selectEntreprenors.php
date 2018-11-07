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
$sql="SELECT * FROM `SG_Entreprenor`";
$result = $con->query($sql);
$row = $result->fetch_array(MYSQLI_ASSOC);

echo "<table>";
echo "<tr><td>Enreprenör</td><td width='10px'></td><td>Id</td>";
echo "<br>";
	
while($row = $result->fetch_assoc())
{
	echo "<tr><td>" . $row["Namn"] . "</td><td width='10px'></td><td>" . $row["ID"] . "</tr>";
}

echo "</table>";

$result->free();
$con->close();
?>
