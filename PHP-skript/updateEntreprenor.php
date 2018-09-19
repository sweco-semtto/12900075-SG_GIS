<?php
$host="sh-mysql-03.active24.com"; // Host name 
$username="DB30034765A"; // Mysql username 
$password="tBHVdMs1"; // Mysql password 
$db_name="DB30034765"; // Database name 
$tbl_name="SG_Entreprenor"; // Table name 

// Connect to server and select database.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

// ID and Text sent from form .NET
$ID=$_POST['ID']; 
$ColumnName=$_POST['ColumnName']; 
$Value=$_POST['Value']; 

// Update
$sql= "UPDATE `$tbl_name` SET `$ColumnName`= '$Value' WHERE `ID` = '$ID'";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
if($rc == 1)
{	
	echo "Updated row " . "'$ID'";	
}
else
{
	echo "Failure to update row " . "'$ID'";
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";

mysql_close();
?>