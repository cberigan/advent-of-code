package com.aoc.problem2;

import java.io.IOException;
import java.util.HashMap;

import com.aoc.helpers.Resources;

public class Problem2 {
	public static void main(String[] args) throws IOException {
		String[] moveSets = Resources.LoadTextFile("prob2.txt").split("\n");
		
		//part 1 
		KeyPadEngine eng = GetKeypadEngine1();
		SubmitMovesToKeyPadEngine(eng, moveSets);
		KeyPadEngine eng2 = GetKeypadEngine2();
		SubmitMovesToKeyPadEngine(eng2, moveSets);
	
	}
	
	
	private static Move GetMove(Character c){
		switch(c){
			case 'U': return Move.Up;
			case 'D': return Move.Down;
			case 'L': return Move.Left;
			case 'R': return Move.Right;
		}
		return null;
	}
	
	private static void SubmitMovesToKeyPadEngine(KeyPadEngine eng, String[] moveSets){
		for(String ms : moveSets){
			for(Character c : ms.toCharArray()){
				Move m = GetMove(c);
				if(m != null){
					eng.SubmitMove(m);
				}
			}
			eng.LogKeyNumber();
		}
		System.out.println("Code " + eng.GetName() + ": " + eng.GetCode());
	}
	
	private static KeyPadEngine GetKeypadEngine1(){
		HashMap<String, Key> keys = new HashMap<String,Key>();
		Key one = new Key("1", null, "4", null, "2");
		Key two = new Key("2", null, "5", "1", "3");
		Key three = new Key("3", null, "6", "2", null);
		Key four = new Key("4", "1", "7", null, "5");
		Key five = new Key("5", "2", "8", "4", "6");
		Key six = new Key("6", "3", "9", "5", null);
		Key seven = new Key("7", "4", null, null, "8");
		Key eight = new Key("8", "5", null, "7", "9");
		Key nine = new Key("9", "6", null, "8", null);
		
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
		return new KeyPadEngine("1", keys,keys.get("5"));
	}
	
	private static KeyPadEngine GetKeypadEngine2(){
		HashMap<String, Key> keys = new HashMap<String,Key>();
		Key one = new Key("1", null, "3", null, null);
		Key two = new Key("2", null, "6", null, "3");
		Key three = new Key("3", "1", "7", "2", "4");
		Key four = new Key("4", null, "8", "3", null);
		Key five = new Key("5", null, null, null, "6");
		Key six = new Key("6", "2", "A", "5", "7");
		Key seven = new Key("7", "3", "B", "6", "8");
		Key eight = new Key("8", "4", "C", "7", "9");
		Key nine = new Key("9", null, null, "8", null);
		Key A = new Key("A", "6", null, null, "B");
		Key B = new Key("B", "7", "D", "A", "C");
		Key C = new Key("C", "8", null, "B", null);
		Key D = new Key("D", "B", null, null, null);
		
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
		keys.put(A.getNumber(), A);
		keys.put(B.getNumber(), B);
		keys.put(C.getNumber(), C);
		keys.put(D.getNumber(), D);
		return new KeyPadEngine("2", keys,keys.get("5"));
	}
}
