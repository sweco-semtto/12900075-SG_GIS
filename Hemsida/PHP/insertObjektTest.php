<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name  
$tbl_name="SG_Test_Objekt"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

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

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);

// Insert Into
$sql="INSERT INTO `$tbl_name` VALUES ('$OrderID' , '$Startplats', '$Objektnr' ,'$Avdnr' ,'$Avdnamn' ,'$Areal_ha' ,'$Giva_KgN_ha', '$Skog_CAN_ton' ,'$Kommentar', '0')";
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
