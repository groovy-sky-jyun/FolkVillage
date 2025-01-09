<?php
	require '../../db/db.php';

	$to_user_id=$_POST["to_user_idPost"];
	$from_user_id=$_POST["from_user_idPost"];
	
	// applyfriend의 are we friend = 1 로 update
	$sql = "UPDATE applyfriend SET are_we_friend = 1 WHERE to_user_id='".$to_user_id."' AND from_user_id='".$from_user_id."'";
	$result = mysqli_query($conn, $sql);
	if($result){
		//friendlist에 추가
		$sql = "INSERT INTO friendlist(user_id, friend_id) VALUES ('".$to_user_id."','".$from_user_id."')";
		$result = mysqli_query($conn, $sql);
		$sql = "INSERT INTO friendlist(user_id, friend_id) VALUES ('".$from_user_id."','".$to_user_id."')";
		$result = mysqli_query($conn, $sql);
		
		//messagelist에 추가
		if(strcmp($to_user_id, $from_user_id)>0){//to user가 더 큼
			$sql = "INSERT INTO messagelist(user1_id, user2_id) VALUES ('".$to_user_id."','".$from_user_id."')";
			$result = mysqli_query($conn, $sql);
		} else{ //from user가 더 큼큼
			$sql = "INSERT INTO messagelist(user1_id, user2_id) VALUES ('".$from_user_id."','".$to_user_id."')";
			$result = mysqli_query($conn, $sql);
		}
		echo "success";
	}		
	else echo "fail";



?>
