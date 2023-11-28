extends Area2D

const SPEED = 400

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta):
	translate(Vector2.UP.rotated(rotation) * SPEED * delta)


func _on_body_entered(body):
	if body.was_shot:
		body.was_shot.emit()
		queue_free()
