extends RigidBody2DWrap

signal has_died

var rotation_speed = TAU
var thrust_force = 400

var fire_cooldown = 0.25
var fire_cooldown_remaining = 0

var invulnerability_duration = 2.0
var invulnerability_remaining = invulnerability_duration

const bullet_scene = preload("res://bullet.tscn")

	
func _physics_process(delta):
	if invulnerability_remaining > 0:
		invulnerability_remaining -= delta
		if invulnerability_remaining > 0:
			$InvulnAnimation.current_animation = "blink"
		else:
			$InvulnAnimation.stop()
	
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


func _on_body_entered(_body):
	if invulnerability_remaining > 0:
		return
		
	has_died.emit()
