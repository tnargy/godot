extends RigidBody2DWrap

signal has_died
signal invuln_ended

var rotation_speed = TAU
var thrust_force = 400

var fire_cooldown = 0.25
var fire_cooldown_remaining = 0

const bullet_scene = preload("res://bullet.tscn")

func _ready():
	$InvulnAnimation.current_animation = "blink"
	
	
func _process(_delta):
	if $InvulnAnimation.is_playing() and (
		Input.is_action_pressed("thrust") or Input.is_action_pressed("fire")):
		invuln_ended.emit()
	
	
func _physics_process(delta):
	angular_velocity = 0
	
	# Movement
	if Input.is_action_pressed("rotate_cw"):
		angular_velocity = rotation_speed
	if Input.is_action_pressed("rotate_ccw"):
		angular_velocity = -1 * rotation_speed
	if Input.is_action_pressed("thrust"):
		apply_force(Vector2.UP.rotated(rotation) * thrust_force)
		
	fire_cooldown_remaining -= delta
	if fire_cooldown_remaining <= 0 and Input.is_action_just_pressed("fire"):
		fire_cooldown_remaining = fire_cooldown
		do_shoot_bullet()
		

func do_shoot_bullet():
	var b = bullet_scene.instantiate()
	b.rotation = self.rotation
	b.position = $Fire.global_position
	get_parent().add_child(b)


func _on_body_entered(body):
	has_died.emit()


func _on_invuln_ended():
	$InvulnAnimation.stop()
	contact_monitor = true
	$Sprite2D.visible = true


func _on_invuln_animation_animation_finished(anim_name):
	if anim_name == "blink":
		invuln_ended.emit()
