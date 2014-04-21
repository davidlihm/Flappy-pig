using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {
	bool isUltra = false;
	public GUIText jump;
	public GUIText time;
	public AudioClip audioUltra;
	public AudioClip audioMove;
	//public AudioClip aoao;
	float timer = 2.0f;
	int count5 = 0;
	float timerFoRotate = 0.8f;
	bool isMove = false;
	// Use this for initialization
	void Start () {
		isStartButtonPressed = false;
		Time.timeScale = 0.0f;
		if (isMove == false) {
			rigidbody.AddForce (new Vector3(175,270,0),ForceMode.Force);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		updateScore();
		updateUltra();
		if (isUltra == true) {
			timerFoRotate -= Time.deltaTime;
			transform.Rotate(Vector3.left*12);
			//transform.Rotate(Vector3.right*-2);
			transform.Rotate(Vector3.down*30);
		}
		if (timer == 0) {
			timerFoRotate = 0.8f;	
			isMove = false;
		}
		if(!isInView())
		{
			restartGame();
		}
		if(Input.GetKeyDown("space") )
		{

			move();
			audio.PlayOneShot(audioMove);
		}
		if (Input.GetKeyDown(KeyCode.R) ){

			ultraMove();	
			audio.PlayOneShot(audioUltra);
		}
	}
	
	private bool isInView()
	{
		Vector3 port = Camera.main.WorldToViewportPoint(transform.position);
		if((port.x < 1) && (port.x > 0) && (port.y < 1) && (port.y > 0) && port.z > 0)
		{
			return true;
		}
		else
		{
			return false;
		}
		
	}
	
	private bool isStartButtonPressed;
	public GUIText scoreLabel;
	void OnGUI()
	{
		if (!isStartButtonPressed)
		{

				Time.timeScale = 1.0f;
				isStartButtonPressed = true;

		}
	}
	
	private void move()
	{
		isMove = true;
		rigidbody.velocity = new Vector3(0,0,0);
		rigidbody.AddForce (new Vector3(175,270,0),ForceMode.Force);

	}
	
	private void updateUltra(){
		count5++;
		count5 = count5 % 5;
		if (isUltra == true) {
			if(this.timer <= 0){
				cancelUltra();
			}else{
				this.timer -= Time.deltaTime;
				if(count5 == 1){
					time.text = timer.ToString("0.00");
				}	
			}		
		}
	}
	
	private void ultraMove()
	{
		int energy = int.Parse (jump.text);
		if (energy > 0 && isUltra == false) {
			isUltra = true;		
			jump.text = (energy - 1).ToString();
			rigidbody.MovePosition(rigidbody.position + new Vector3(1,1,-2));
			rigidbody.velocity = new Vector3(0,0,0);
			rigidbody.AddForce (new Vector3(175,270,0),ForceMode.Force);

		}
		
	}
	
	private void cancelUltra()
	{
		rigidbody.MovePosition(rigidbody.position + new Vector3(0,0,2));
		rigidbody.velocity = new Vector3(0,0,0);
		rigidbody.AddForce (new Vector3(175,270,0),ForceMode.Force);
		transform.eulerAngles = new Vector3(90,270,0);
		isUltra = false;
		time.text = "NONE";
		this.timer = 2.0f; 
	}
	
	void OnTriggerEnter(Collider other)
	{

		restartGame();
	}
	private void restartGame()
	{
		Time.timeScale = 0.0f;
		//audio.PlayOneShot(aoao);
		
		isStartButtonPressed = false;
		Application.LoadLevel ("die");	
		
	}
	
	private void updateScore()
	{
		int score = (int) (transform.position.x / GenerateWorld.distanceBetweenObjects);
		if(score != (int.Parse(scoreLabel.text)) && score > 0)
		{
			scoreLabel.text = score.ToString();
		}
		
	}
}
