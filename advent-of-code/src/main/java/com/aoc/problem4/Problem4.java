package com.aoc.problem4;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;

import com.aoc.helpers.Resources;

public class Problem4 {

	public static void main(String[] args) throws IOException {
		String[] raw = Resources.LoadTextFile("prob4.txt").split("\n");
		Integer sum = 0;
		for(String line : raw){
			
			String data = line.substring(0, line.indexOf('['));
			String[] blocks = data.split("-");
			
			Integer id = Integer.parseInt(blocks[blocks.length-1]);
			String cs = line.substring(line.indexOf('[')+1, line.indexOf(']'));
			String decrypted = cipher(data,id % 26);
			CharCountPair[] cc = GetCharCounts(blocks);
			CharCountPair[] top5 = GetTop5(cc);
			
			if(IsValid(top5,cs)){
				sum+=id;
			}
			if(decrypted.startsWith("north")){
				System.out.println(decrypted);
			}
		}
		
		System.out.println("sum of ids: " + sum);
	}
	
	private static String cipher(String msg, int shift){
	    String s = "";
	    int len = msg.length();
	    for(int x = 0; x < len; x++){
	    	if(Character.isDigit(msg.charAt(x))){
	    		s+= msg.charAt(x);
	    		continue;
	    	}
	    	if(msg.charAt(x) == '-'){
	    		s+= ' ';
	    		continue;
	    	}
	        char c = (char)(msg.charAt(x) + shift);
	        if (c > 'z')
	            s += (char)(msg.charAt(x) - (26-shift));
	        else
	            s += (char)(msg.charAt(x) + shift);
	    }
	    return s;
	}

	private static boolean IsValid(CharCountPair[] top5, String cs) {
		boolean isValid = true;
		char[] ccChars = cs.toCharArray();
		for(int i = 0; i < top5.length;i++){
			if(ccChars[i] != top5[i].getChar()){
				isValid = false;
			}
		}
		return isValid;
	}

	private static Integer GetMinCount(CharCountPair[] cp){
		Integer min = 100;
		for(CharCountPair c : cp){
			if(c.getCount() < min){
				min = c.getCount();
			}
		}
		return min;
	}
	
	private static Integer GetMaxCount(CharCountPair[] cp){
		Integer max = 0;
		for(CharCountPair c : cp){
			if(c.getCount() > max){
				max = c.getCount();
			}
		}
		return max;
	}
	
	private static CharCountPair[] GetTop5(CharCountPair[] charCounts){
		Integer min = GetMinCount(charCounts);
		Integer max = GetMaxCount(charCounts);
		List<CharCountPair> top = new ArrayList<CharCountPair>();
		for(Integer i = max; i >= min;i--){
			//get all chars with i count
			CharCountPair[] byCount = GetByCountSorted(charCounts,i);
			for(CharCountPair p : byCount){
				if(top.size() < 5){
					top.add(p);
				}else{
					return top.toArray(new CharCountPair[5]);
				}
			}
		}
		return null;
	}
	
	private static CharCountPair[] GetByCountSorted(CharCountPair[] p, Integer count){
		List<CharCountPair> cp = new ArrayList<CharCountPair>();
		for(CharCountPair c : p){
			if(c.getCount() ==count){
				cp.add(c);
			}
		}
		CharCountPair[] result = cp.toArray(new CharCountPair[cp.size()]);
		Arrays.sort(result, new CharComparer());
		return result;
	}
	
	private static CharCountPair[] GetCharCounts(String[] blocks){
		HashMap<Character,CharCountPair> counts = new HashMap<Character,CharCountPair>();
		String[] letters = Arrays.copyOfRange(blocks, 0, blocks.length - 1);
		for(String s : letters){
			for(Character c : s.toCharArray()){
				if(counts.containsKey(c)){
					CharCountPair cp = counts.get(c);
					cp.incCount();
					counts.put(c, cp);
				}else{
					counts.put(c, new CharCountPair(c,1));
				}
			}
		}
		return (new ArrayList<CharCountPair>(counts.values())).toArray(new CharCountPair[counts.keySet().size()]);
	}
}
