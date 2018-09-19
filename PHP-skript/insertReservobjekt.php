<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Reservobjekt"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$OrderID     =$_POST['OrderID']; 
$Objektnr    =$_POST['Objektnr'];
$Avdnr       =$_POST['Avdnr'];
$Avdnamn     =$_POST['Avdnamn'];
$Areal_ha    =$_POST['Areal_ha'];
$Giva_KgN_ha =$_POST['Giva_KgN_ha'];
$Kommentar   =$_POST['Kommentar'];

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES ('$OrderID' ,'$Objektnr' ,'$Avdnr' ,'$Avdnamn' ,'$Areal_ha' ,'$Giva_KgN_ha' ,'$Kommentar')";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

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

mysql_close();
?>
