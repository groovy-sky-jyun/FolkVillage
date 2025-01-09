<?php
	require '../../db/db.php';

	$nickname=$_POST["nicknamePost"];
	
	$sql = "SELECT gender,id FROM userinfo WHERE nickname = '".$nickname."' ";
	$result = mysqli_query($conn, $sql);
	
	if(mysqli_num_rows($result)>0)
	{
		$row = mysqli_fetch_assoc($result);
		echo $row['gender'].','.$row['id'];
	} 
	else 
	{
	  echo "no user";
	}

?>
