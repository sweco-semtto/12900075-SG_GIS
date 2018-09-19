<?php 
	$host="mysql410.loopia.se"; // Host name 
	$username="UNLYdfR9@s142821"; // Mysql username 
	$password="2ykgB03hnx"; // Mysql password 
	$db_name="sg_systemet_com"; // Database name 
	$tbl_name="SG_OrderBackup"; // Table name 

	// Connect to server and select database.
	mysql_connect("$host", "$username", "$password")or die("cannot connect"); 
	mysql_select_db("$db_name")or die("cannot select DB");

	# Hämtar data ifrån REQUEST
	$time = $_REQUEST["time"];
	$ordernummer = $_REQUEST["ordernummer"];
	$xml = $_REQUEST["xml"];

	# Ersätter alla &- och =-tecken (om order backupas från .NET). 
	$xml = str_replace("|ampersand|", "&", $xml);
	$xml = str_replace("|equals|", "=", $xml);
	
	# Ersätter alla å, ä, ö m.fl.
	$xml = str_replace("&aring", "å", $xml);
	$xml = str_replace("&Aring", "Å", $xml);
	$xml = str_replace("&auml", "ä", $xml);
	$xml = str_replace("&Auml", "Ä", $xml);
	$xml = str_replace("&ouml", "ö", $xml);
	$xml = str_replace("&Ouml", "Ö", $xml);
	$xml = str_replace("&uuml", "ü", $xml);
	$xml = str_replace("&Uuml", "Ü", $xml);
	$xml = str_replace("&ucirc", "û", $xml);
	$xml = str_replace("&Ucirc", "Û", $xml);
	$xml = str_replace("&egrave", "é", $xml);
	$xml = str_replace("&Egrave", "É", $xml);
	$xml = str_replace("&eacute", "è", $xml);
	$xml = str_replace("&Eacute", "È", $xml);
	$xml = str_replace("&amp", "&", $xml);
	$xml = str_replace("&lt", "<", $xml);
	$xml = str_replace("&gt", ">", $xml);
	$xml = str_replace("&quot", '"', $xml);
	$xml = str_replace("&#39", "'", $xml);

	# Skriver till backup av beställningen till databasen. 
	$sql="INSERT INTO `$tbl_name` (Datum, Ordernummer, XML) VALUES ('$time', '$ordernummer', '$xml')";
	$result=mysql_query($sql);
	$affectedRows = mysql_affected_rows();

	// Kontrollerar att en rad skapades och returnerar true eller false beroende om raden skapades eller inte. 
	if ($affectedRows == 1)
	{
		echo "true";
	}
	else
	{
		echo "false";
	}
?>