<?php
	require '../../db/db.php';
	
	//db컬럼
	$user_id=$_POST["user_idPost"];
	$friend_id=$_POST["friend_idPost"];
	
	$sql = "UPDATE friendlist SET accept_gift= 1 WHERE  user_id= '".$user_id."' and friend_id= '".$friend_id."'";
	$result = mysqli_query($conn, $sql);
	if($result)
	{
		echo "Friendlist accept_gift update success";
	}
	else
		echo"fail"
?>
