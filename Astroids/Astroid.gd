extends RigidBody2DWrap

signal was_shot

const STARTING_FORCE = 50

@export var debris_scene : PackedScene
@export var debris_amount = 2

func _ready():
	apply_impulse(Utility.RandomUnitVector2() * randf_range(STARTING_FORCE/2.0, STARTING_FORCE*2.0))


func _on_was_shot():
	if debris_scene:
		for i in debris_amount:
			var d = debris_scene.instantiate()
			d.global_position = global_position
			get_parent().call_deferred("add_child",d)
	queue_free()
