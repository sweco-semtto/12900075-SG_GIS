<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Startplats"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($con->connect_errno) {
    printf("Anslutningsfel: %s\n", $con->connect_error);
    exit();
}

// ID and Text sent from form .NET
$ID = $_POST['ID']; 
$OrderID=$_POST['OrderID']; 
$ColumnName=$_POST['ColumnName']; 
$Value=$_POST['Value']; 

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";

// Select
$sql= "SELECT * FROM `$tbl_name` WHERE `$ColumnName`= '$Value' and `ID` = '$ID'";
$result = $con->query($sql);
$rc = $con->affected_rows;
if($rc >= 1)
{	
	echo "No changes to row " . "'$OrderID'";
}
else
{
	// Update
	$sql= "UPDATE `$tbl_name` SET `$ColumnName`= '$Value' WHERE `ID` = '$ID'";
	$result = $con->query($sql);
	$rc = $con->affected_rows;

	if($rc >= 1)
	{	
		echo "Updated row " . "'$OrderID'";	
	}
	else
	{
		echo "Failure to update row " . "'$OrderID'";
		echo "\n";
		echo "'$sql'";
	}
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

//$result->free();
//$con->close();
?>