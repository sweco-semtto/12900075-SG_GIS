<?php 
$host="mysql410.loopia.se"; // Host name 
$username="UNLYdfR9@s142821"; // Mysql username 
$password="2ykgB03hnx"; // Mysql password 
$db_name="sg_systemet_com"; // Database name 
$tbl_name="SG_OrderBackup"; // Table name 

// Anger att det �r text som skall produceras. 
header('Content-type: text/plain');

// Connect to server and select databse.
$con = mysqli_connect($host, $username, $password, $db_name);
if ($mysqli->connect_errno) {
	printf("Anslutningsfel: %s\n", $mysqli->connect_error);
	exit();
}	

// H�mtar data ifr�n REQUEST
$time = $_REQUEST["time"];
$ordernummer = $_REQUEST["ordernummer"];
$xml = $_REQUEST["xml"];

// Ers�tter alla &- och =-tecken (om order backupas fr�n .NET). 
$xml = str_replace("|ampersand|", "&", $xml);
$xml = str_replace("|equals|", "=", $xml);

// Ers�tter alla �, �, � m.fl.
$xml = str_replace("&aring", "�", $xml);
$xml = str_replace("&Aring", "�", $xml);
$xml = str_replace("&auml", "�", $xml);
$xml = str_replace("&Auml", "�", $xml);
$xml = str_replace("&ouml", "�", $xml);
$xml = str_replace("&Ouml", "�", $xml);
$xml = str_replace("&uuml", "�", $xml);
$xml = str_replace("&Uuml", "�", $xml);
$xml = str_replace("&ucirc", "�", $xml);
$xml = str_replace("&Ucirc", "�", $xml);
$xml = str_replace("&egrave", "�", $xml);
$xml = str_replace("&Egrave", "�", $xml);
$xml = str_replace("&eacute", "�", $xml);
$xml = str_replace("&Eacute", "�", $xml);
$xml = str_replace("&amp", "&", $xml);
$xml = str_replace("&lt", "<", $xml);
$xml = str_replace("&gt", ">", $xml);
$xml = str_replace("&quot", '"', $xml);
$xml = str_replace("&#39", "'", $xml);

// Skriver till backup av best�llningen till databasen. 
$sql="INSERT INTO `$tbl_name` (Datum, Ordernummer, XML) VALUES ('$time', '$ordernummer', '$xml')";
$result = $con->query($sql);
$rc = $con->affected_rows;

// Kontrollerar att en rad skapades och returnerar true eller false beroende om raden skapades eller inte. 
if ($rc == 1)
{
	echo "true";
}
else
{
	echo "false";
}
?>