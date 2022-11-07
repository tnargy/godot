extends AnimatedSprite


func _ready():
	set_playing(true)
	

func _on_GunImpact_animation_finished():
	queue_free()
