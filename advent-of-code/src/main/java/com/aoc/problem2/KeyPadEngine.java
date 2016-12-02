package com.aoc.problem2;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

public class KeyPadEngine {

	private HashMap<Integer, Key> keys;
	private Key position;
	
	private List<Integer> code;
	
	public KeyPadEngine(){
		//set up keypad
		keys = new HashMap<Integer, Key>();
		code = new ArrayList<Integer>();
		
		Key one = new Key(1, null, 4, null, 2);
		Key two = new Key(2, null, 5, 1, 3);
		Key three = new Key(3, null, 6, 2, null);
		Key four = new Key(4, 1, 7, null, 5);
		Key five = new Key(5, 2, 8, 4, 6);
		Key six = new Key(6, 3, 9, 5, null);
		Key seven = new Key(7, 4, null, null, 8);
		Key eight = new Key(8, 5, null, 7, 9);
		Key nine = new Key(9, 6, null, 8, null);
		
		//load into key set
		keys.put(one.getNumber(), one);
		keys.put(two.getNumber(), two);
		keys.put(three.getNumber(), three);
		keys.put(four.getNumber(), four);
		keys.put(five.getNumber(), five);
		keys.put(six.getNumber(), six);
		keys.put(seven.getNumber(), seven);
		keys.put(eight.getNumber(), eight);
		keys.put(nine.getNumber(), nine);
		
		//set initial position
		position = five;
	}
	
	public void SubmitMove(Move m){
		Integer newKey = null;
		switch(m){
			case Up: newKey = position.getUp();
			break;
			case Down: newKey = position.getDown();
			break;
			case Left: newKey = position.getLeft();
			break;
			case Right: newKey = position.getRight();
			break;
		}
		if(newKey != null){
			position = keys.get(newKey);
		}
	}
	
	public void LogKeyNumber(){
		code.add(position.getNumber());
	}
	
	public String GetCode(){
		StringBuilder sb = new StringBuilder();
		for(Integer i : code){
			sb.append(i);
		}
		return sb.toString();
	}
}
