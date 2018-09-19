<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Reservobjekt"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$OrderID=$_POST['OrderID']; 
$ColumnName=$_POST['ColumnName']; 
$Value=$_POST['Value']; 

// Update
//$sql= "UPDATE `SG_Reservobjekt` SET `Avdnamn`= '$Value' WHERE `OrderID` = '$OrderID'";
$sql= "UPDATE `$tbl_name` SET `$ColumnName`= '$Value' WHERE `OrderID` = '$OrderID'";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

/*echo "$sql";
echo "\n";
echo "\n";*/

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
if($rc == 1)
{	
	echo "Updated row " . "'$OrderID'";	
}
else
{
	echo "Failure to update row " . "'$OrderID'";
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>