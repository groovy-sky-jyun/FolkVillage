<?php
	require '../../db/db.php';
	
	$user_id=$_POST["user_idPost"];
	$user_coin=$_POST["user_coinPost"];
	
	$sql = "UPDATE userinfo SET coin='".$user_coin."' WHERE  id= '".$user_id."'";
	$result = mysqli_query($conn, $sql);
	if($result)
	{
		echo "success";
	}
	else
		echo"fail"
?>
