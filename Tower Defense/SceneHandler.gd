extends Node

func _ready():
	load_main_menu()

func load_main_menu():
	get_node("MainMenu/M/VB/NewGame").connect("pressed", Callable(self, "on_new_game_pressed"))
	get_node("MainMenu/M/VB/Quit").connect("pressed", Callable(self, "on_quit_pressed"))
	
func on_new_game_pressed():
	get_node("MainMenu").queue_free()
	var game_scene = load("res://Scenes/MainScenes/GameScene.tscn").instantiate()
	game_scene.connect("game_finished", Callable(self, 'unload_game'))
	add_child(game_scene)
	
func on_quit_pressed():
	get_tree().quit()

func unload_game(result):
	get_node("GameScene").queue_free()
	var main_menu = load("res://Scenes/UIScenes/MainMenu.tscn").instantiate()
	add_child(main_menu)
	load_main_menu()
