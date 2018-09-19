<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Test_Startplats"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$ID               = $_POST['ID'];
$ID_Access		  = $_POST['ID_Access'];
$OrderID          = $_POST['OrderID']; 
$Startplats       = $_POST['Startplats']; 
$Nordligkoordinat = $_POST['Nordligkoordinat_startplats'];
$Ostligkoordinat  = $_POST['Ostligkoordinat_startplats'];
$Areal_ha         = $_POST['Areal_ha_startplats'];
$Skog_CAN_ton     = $_POST['Skog_CAN_ton_startplats'];
$Ingaende_Objekt  = $_POST['Ingaende_Objekt'];
$Status           = $_POST['Status'];

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES ('$ID', '$ID_Access', '$OrderID' , '$Startplats', '$Nordligkoordinat' ,'$Ostligkoordinat' ,'$Areal_ha' , '$Skog_CAN_ton' ,'$Ingaende_Objekt', '$Status', '0', '0', '0')";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "\n";
echo "<Data>";
if ($rc == 1) // i.e. ($num_rows_after - $num_rows_before == 1)	
{
	echo "Success, inserted " . "'$OrderID'";
}
else
{
	echo "Failure, not inserted " . "'$OrderID'";
	echo "\n";
	echo $sql;
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>
