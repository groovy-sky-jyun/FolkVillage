<?php
	require '../../db/db.php';

	$user_id=$_POST["user_idPost"];
	$friend_id=$_POST["friend_idPost"];
	$coin = $_POST["user_coin"];
	
	$sql ="UPDATE friendlist AS A, userinfo AS B SET  A.accept_gift = 1, B.coin = '".$coin."' WHERE  A.friend_id = '".$user_id."' AND A.user_id='".$friend_id."' AND  B.id = '".$user_id."'"; 
	$result = mysqli_query($conn, $sql);

	if($result==1)
	{
		echo "coin update success";
		
	} else {
		  echo "fail";
		}
?>
