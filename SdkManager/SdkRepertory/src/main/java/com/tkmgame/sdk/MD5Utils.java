package com.tkmgame.sdk;

import java.security.MessageDigest;

public class MD5Utils {

	public static String md5(String str) {
        if (str == null) {
            return null;
        }
        try {
            MessageDigest messageDigest = MessageDigest.getInstance("MD5");
            messageDigest.update(str.getBytes());
            return new String(encodeHex(messageDigest.digest()));
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

	 public static char[] encodeHex(final byte[] data) {
	     final int l = data.length;
	     final char[] out = new char[l << 1];
	     // two characters form the hex value.
	     for (int i = 0, j = 0; i < l; i++) {
	         out[j++] = DIGITS_LOWER[(0xF0 & data[i]) >>> 4];
	         out[j++] = DIGITS_LOWER[0x0F & data[i]];
	     }
	     return out;
	 }

	private static final char[] DIGITS_LOWER = 
		{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
	
	
}
