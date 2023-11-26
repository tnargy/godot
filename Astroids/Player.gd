extends RigidBody2D

var rotation_speed = TAU
var thrust_speed = 400

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	# Movement
	if Input.is_action_pressed("rotate_cw"):
		rotate(rotation_speed  * delta)
	if Input.is_action_pressed("rotate_ccw"):
		rotate(-1 * rotation_speed  * delta)
	if Input.is_action_pressed("thrust"):
		translate(Vector2.UP.rotated(rotation) * thrust_speed * delta )
		
	# Fire
	if Input.is_action_pressed("fire"):
		pass
	pass
