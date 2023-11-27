extends RigidBody2DWrap

signal has_died
signal invuln_ended

var rotation_speed = TAU
var thrust_force = 400

func _ready():
	$InvulnAnimation.current_animation = "blink"
	
	
func _process(_delta):
	if $InvulnAnimation.is_playing() and (
		Input.is_action_pressed("thrust") or Input.is_action_pressed("fire")):
		invuln_ended.emit()
	
	
func _physics_process(_delta):
	angular_velocity = 0
	
	# Movement
	if Input.is_action_pressed("rotate_cw"):
		angular_velocity = rotation_speed
	if Input.is_action_pressed("rotate_ccw"):
		angular_velocity = -1 * rotation_speed
	if Input.is_action_pressed("thrust"):
		apply_force(Vector2.UP.rotated(rotation) * thrust_force)
		


func _on_body_entered(body):
	print(name + " was hit by " + body.name)
	has_died.emit()


func _on_invuln_ended():
	print("Vulnerable")
	$InvulnAnimation.stop()
	contact_monitor = true
	$Sprite2D.visible = true


func _on_invuln_animation_animation_finished(anim_name):
	if anim_name == "blink":
		invuln_ended.emit()
