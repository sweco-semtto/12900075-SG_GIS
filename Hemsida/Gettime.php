<?php 
	$now = DateTime::createFromFormat('U.u', microtime(true));
	echo $now->format("Y-m-d H:i:s u");
?>