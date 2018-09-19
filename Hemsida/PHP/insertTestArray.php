<?php
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_Test"; // Table name 

// Connect to server and select databse.
mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
mysql_select_db("$db_name")or die("cannot select DB");

$Id = "a";
$Text = "b";

// ID and Text sent from form .NET
//$array=$_POST['array']; 

$array = ("ID" = > "5", "Text" => "MNO");

foreach ($array as $i) 
{
	$Id = $i['ID'];
	$Text = $['Text'];
	
	echo "'$ID'" . "   " . "'$Text'";
}

// To protect MySQL injection (more detail about MySQL injection)
//$myID = stripslashes($myID);
//$myText = stripslashes($myText);
//$myID = mysql_real_escape_string($myID);
//$myText = mysql_real_escape_string($myText);
/*
// Insert Into
$sql="INSERT INTO `$tbl_name` (ID, Text) VALUES ('$myID' ,'$myText')";
$result=mysql_query($sql);
$rc = mysql_affected_rows();

// Checks if success
echo "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
echo "\n";
echo "<MessageXML>";
echo "\n";
echo "<Data>";
echo "\n";
if ($rc == 1)//($num_rows_after - $num_rows_before == 1)	
{
	echo "Success, inserted " . "'$myID'";
}
else
{
	echo "Failure, not inserted " . "'$myID'";
}
echo "\n";
echo "</Data>";
echo "\n";
echo "</MessageXML>";
*/
mysql_close();
?>
