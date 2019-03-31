<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test_Startplats"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($con->connect_errno) {
    printf("Anslutningsfel: %s\n", $con->connect_error);
    exit();
}

// Data sent from form .NET
$OrderID=$_POST['OrderID'];

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";

// Select
$sql= "SELECT * FROM `$tbl_name` WHERE Borttagen = 1 and `OrderID` = '$OrderID'";
$result = $con->query($sql);
$rc = $con->affected_rows;
if($rc >= 1)
{	
	echo "No changes to row " . "'$OrderID'";
}
else
{
	//Delete sker genom att flagga som borttagen
	$sql="UPDATE `$tbl_name` SET Borttagen = 1 WHERE OrderID = '$OrderID'";
	$result = $con->query($sql);
	$rc = $con->affected_rows;

	if ($rc >= 1)	
	{
		echo "Success, deleted ID " . "'$OrderID'";
	}
	else
	{
		echo "Failure, not deleted ID " . "'$OrderID'";
	}
}

echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

//$result->free();
//$con->close();
?>