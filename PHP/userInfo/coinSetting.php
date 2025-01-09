<?php
	require '../../db/db.php';
	
	//db컬럼
	$user_id=$_POST["user_idPost"];

	$sql = "SELECT coin FROM userinfo WHERE  id= '".$user_id."' ";
	$result = mysqli_query($conn, $sql);
	if(mysqli_num_rows($result)>0)
	{
		$row = mysqli_fetch_assoc($result);
		echo $row['coin'];
	}
	else
		echo"fail"
	

?>
