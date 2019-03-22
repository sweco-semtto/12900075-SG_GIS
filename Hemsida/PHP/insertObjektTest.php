<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name  
$tbl_name="SG_Test_Objekt"; // Table name 

// Anger att det är text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($mysqli->connect_errno) {
    printf("Anslutningsfel: %s\n", $mysqli->connect_error);
    exit();
}

// ID and Text sent from form .NET
$OrderID      =$_POST['OrderID']; 
$Startplats   =$_POST['Startplats']; 
$Objektnr     =$_POST['Objektnr'];
$Avdnr        =$_POST['Avdnr'];
$Avdnamn      =$_POST['Avdnamn'];
$Areal_ha     =$_POST['Areal_ha'];
$Giva_KgN_ha  =$_POST['Giva_KgN_ha'];
$Skog_CAN_ton =$_POST['Skog_CAN_ton'];
$Kommentar    =$_POST['Kommentar'];

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES ('$OrderID' , '$Startplats', '$Objektnr' ,'$Avdnr' ,'$Avdnamn' ,'$Areal_ha' ,'$Giva_KgN_ha', '$Skog_CAN_ton' ,'$Kommentar', '0')";
$result = $con->query($sql);
$rc = $con->affected_rows;

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";
if ($rc == 1) // i.e. ($num_rows_after - $num_rows_before == 1)	
{
	echo "Success, inserted " . "'$OrderID'";
}
else
{
	echo "Failure, not inserted " . "'$OrderID'";
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

//$result->free();
//$con->close();
?>