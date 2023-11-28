extends RigidBody2DWrap

signal was_shot

const STARTING_FORCE = 50

func _ready():
	apply_impulse(Utility.RandomUnitVector2() * randf_range(STARTING_FORCE/2.0, STARTING_FORCE*2.0))


func _on_was_shot():
	queue_free()
