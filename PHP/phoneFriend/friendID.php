<?php
	require '../../db/db.php';

	$nickname=$_POST["nicknamePost"];

	$sql = "SELECT id FROM userinfo WHERE  nickname= '".$nickname."' ";
	$result = mysqli_query($conn, $sql);
	if(mysqli_num_rows($result)>0)
	{
		$row = mysqli_fetch_assoc($result);
		echo $row['id'];
	}
	else
		echo"fail"
	

?>
