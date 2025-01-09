<?php
	require '../../db/db.php';

	$friend_id=$_POST["friendIDPost"];
	
	$sql = "SELECT nickname FROM userinfo WHERE  id= '".$friend_id."' ";
	$result = mysqli_query($conn, $sql);
	if(mysqli_num_rows($result)>0)
	{
		$row = mysqli_fetch_assoc($result);
		echo $row['nickname'];
	}
	else
		echo"fail"
?>
