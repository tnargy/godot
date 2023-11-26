extends RigidBody2D

var rotation_speed = TAU
var thrust_force = 400

@onready var viewport_size = get_viewport().size
@onready var sprite_size = $Sprite2D.get_rect().size

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	angular_velocity = 0
	
	# Movement
	if Input.is_action_pressed("rotate_cw"):
		angular_velocity = rotation_speed
	if Input.is_action_pressed("rotate_ccw"):
		angular_velocity = -1 * rotation_speed
	if Input.is_action_pressed("thrust"):
		apply_force(Vector2.UP.rotated(rotation) * thrust_force)
		

func _integrate_forces(state):
	wrap_to_viewport(state)


func wrap_to_viewport(state):
	# Left
	if state.transform.origin.x + sprite_size.x/2 < 0:
		state.transform.origin.x = viewport_size.x
	# Right
	if state.transform.origin.x - sprite_size.x/2> viewport_size.x:
		state.transform.origin.x = -sprite_size.x/2
	# Up
	if state.transform.origin.y + sprite_size.y/2 < 0:
		state.transform.origin.y = viewport_size.y
	# Down
	if state.transform.origin.y - sprite_size.y/2 > viewport_size.y:
		state.transform.origin.y = -sprite_size.y/2
	pass
