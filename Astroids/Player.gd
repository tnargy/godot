extends RigidBody2D

var rotation_speed = TAU
var thrust_force = 400

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


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
