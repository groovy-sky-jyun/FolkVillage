
<?php
	require '../../db/db.php';

	$user_id=$_POST["user_idPost"];
	$friend_id=$_POST["friend_idPost"];
	$message_text=$_POST["textPost"];

	//friendlist update
	$sql = "UPDATE friendlist SET send_gift = 1 WHERE user_id= '".$user_id."' and  friend_id= '".$friend_id."'";
	$result = mysqli_query($conn, $sql);
	if($result)
	{
		//get message number and count for messagelist
		if($user_id>$friend_id){
			$sql="SELECT * FROM messagelist WHERE user1_id='".$user_id."' and user2_id='".$friend_id."'";
		}
		else{
			$sql="SELECT * FROM messagelist WHERE user1_id='".$friend_id."' and user2_id='".$user_id."'";
		}
		$result=mysqli_query($conn, $sql);
		if(mysqli_num_rows($result)> 0){
			$row=mysqli_fetch_array($result);
			$number = $row["number"];
			$message_count=(int)$row['message_count']+1;

			//add messagerecord
			$sql="INSERT INTO messagerecord(list_number, sender_id, receiver_id, message_txt, message_order)VALUES('".$number."','".$user_id."','".$friend_id."','".$message_text."','".$message_count."')";
			$result = mysqli_query($conn, $sql);
			if($result){
				//update messagelist message_count
				$sql="UPDATE messagelist SET message_count=$message_count WHERE number = '".$number."'";
				$result=mysqli_query($conn, $sql);
				if($result)
					echo "heart send success";
				else echo "fail";
			}
			else echo"fail";
		}else echo"fail";	
	}
	else
		echo"fail";
	

?>
