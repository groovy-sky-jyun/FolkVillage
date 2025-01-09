
<?php
	require '../../db/db.php';
	
	header('Content-Type: application/json');
	$rawData = file_get_contents("php://input");
	$data = json_decode($rawData, true);
	
	if (isset($data['tableNumberPost']) && isset($data['lastCountPost'])) {
		$table_number = $data['tableNumberPost'];
		$count = $data['lastCountPost'];

		// 1. number가 일치하는 것충 가장 마지막메시지 선택
	   $sql = "SELECT * FROM messagerecord WHERE list_number = '".$table_number."' and message_order='".$count."'";
	   $result = mysqli_query($conn, $sql);	
   
	   if(mysqli_num_rows($result)>0){
		   while($row = mysqli_fetch_assoc($result)){
			   $dataTable[]=array(
				   'sender_id' => $row['sender_id'],
				   'receiver_id' => $row['receiver_id'],
				   'message_txt' => $row['message_txt'],
				   'message_time' => $row['message_time'],
				   'read_check' => $row['read_check']
			   );
		   }
		   $status = 'success';
		   $arr = array("status"=>$status, "data"=>$dataTable);
		   echo json_encode($arr, JSON_UNESCAPED_UNICODE);
	   }
	   else{
		   $status = 'fail';
		   $dataTable[] = array(
			   'sender_id' => $row['sender_id'],
			   'receiver_id' => $row['receiver_id'],
			   'message_txt' => $row['fail'],
			   'message_time' => $row['fail'],
			   'read_check' => $row['fail']
		   );
		   $arr = array("status"=>$status, "data"=>$dataTable);
		   echo json_encode($arr);
	   }
	}
	
?>
