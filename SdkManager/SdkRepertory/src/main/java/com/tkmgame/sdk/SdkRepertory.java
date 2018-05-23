//
// Source code recreated from a .class file by IntelliJ IDEA
// (powered by Fernflower decompiler)
//

package com.tkmgame.sdk;

import android.app.Fragment;
import android.os.Bundle;

import cn.jj.jjgamesdk.ITKAPICallback;
import cn.jj.jjgamesdk.TKAPI;

import com.unity3d.player.UnityPlayer;

import java.util.Locale;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class SdkRepertory extends Fragment {
    private static final int APPID = 2;
    private static final String TAG = "MyPlugin";
    private static SdkRepertory s_instance = null;

    private int figureID = 0;
    private String goodsParam = "";
    private boolean isPasswordExist = false;
    private boolean isStart = true;
    private boolean isTest = true;

    private String lobbyStartParam = "";
    private String m_gameObjectName;
    private TKAPI m_tkapi = null;
    private int modifyNicknameLevelID = -1;

    public SdkRepertory() {
    }

    public static SdkRepertory instance(String var0) {
        if (s_instance == null) {
            s_instance = new SdkRepertory();
            s_instance.m_gameObjectName = var0;
            UnityPlayer.currentActivity.getFragmentManager().beginTransaction().add(s_instance, "MyPlugin").commit();
        }

        return s_instance;
    }

    public void onCreate(Bundle var1) {
        super.onCreate(var1);
        this.setRetainInstance(true);
        this.m_tkapi = TKAPI.getInstance(this.getActivity().getApplicationContext());
        if (this.isTest) {
            this.m_tkapi.openTestMode();
        }

        try {
            this.m_tkapi.init(2);
        } catch (Exception var2) {
            ;
        }
        this.m_tkapi.setITKAPICallback(this.itkCallback);
        m_tkapi.open();
    }

    private void payOrder(String var1) {
        this.infoShow("payOrder---goodsInfo:" + var1);

        try {
            JSONObject var6 = new JSONObject(var1);
            String var3 = String.valueOf(System.currentTimeMillis());
            int var2 = (int) (System.currentTimeMillis() / 1000L);
            String var4 = String.format(Locale.getDefault(), "%d|%s|%d|%d|%d|%d|%d|%d|%d|%d|%d|%s", new Object[]{Integer.valueOf(2), var3, Integer.valueOf(var6.optInt("AppSchemeID", 0)), Integer.valueOf(var2), Integer.valueOf(var6.optInt("PayMethodID", 0)), Integer.valueOf(var6.optInt("QuotationType", 0)), Integer.valueOf(var6.optInt("GoodsID", 0)), Integer.valueOf(var6.optInt("GoodsAmount", 0)), Integer.valueOf(var6.optInt("MoneyType", 0)), Integer.valueOf(var6.optInt("MoneyAmount", 0)), Integer.valueOf(var6.optInt("ECASchemeID", 0)), "1erah41234510cj1amk29iuxbzc3vwrt"});
            this.log("sign:" + var4);
            var4 = MD5Utils.md5(var4);
            var6.put("AppOrder", var3);
            var6.put("AppExtendData", "appextenddata");
            var6.put("AppReqTime", var2);
            var6.put("AppOrderSign", var4);
            var2 = this.m_tkapi.payOrder(var6.toString(), this.getActivity());
            this.infoShow("payOrder:" + var2);
        } catch (JSONException var5) {
            var5.printStackTrace();
        }
    }

    public int CalculateAdd(int var1, int var2) {
        return var1 + var2;
    }

    public void SayHello() {
        UnityPlayer.UnitySendMessage(this.m_gameObjectName, "showText", "Hello Unity!");
    }

    public void infoShow(String var1) {
        this.showText("mx_log", var1);
    }

    public void log(String var1) {
        this.showText("mx_log", var1);
    }

    //region 你的描述
    public int loginQuick() {
        int ret = m_tkapi.loginWithGuestAccount();
        infoShow("loginWithGuestAccount ret:" + ret);
        return ret;
    }

    public int logout() {
        int ret = m_tkapi.logout();
        infoShow("logout ret:" + ret);
        return ret;
    }

    public int register(String user, String pw, String code, String name) {
        int ret = m_tkapi.register(user, pw, code, name);
        infoShow("register ret:" + ret);
        return ret;
    }

    public int password() {
        return 0;
    }

    public int loginWeChat() {
        if (this.m_tkapi.isWeChatInstalled()) {
            this.showLog("微信登录需要真实包名和签名");
            int var1 = this.m_tkapi.loginWithWeChat("wx1850d17139d5fab3", 16);
            this.showLog("loginWithWeChat ret:" + var1);
            return var1;
        }
        return -1;
    }
    //endregion

    public void showLog(String var1) {
        this.showText("mx_log", var1);
    }

    public void showText(String var1, String var2) {
        UnityPlayer.UnitySendMessage(var1, "showText", var2);
    }

    private ITKAPICallback itkCallback = new ITKAPICallback() {
        public void onConnectFailed() {
            SdkRepertory.this.log("onConnectFailed");
        }

        public void onConnectSuccess() {
            SdkRepertory.this.infoShow("onConnectSuccess");
            if (SdkRepertory.this.isStart) {
                SdkRepertory.this.infoShow("onConnectSuccess---current login status:" + SdkRepertory.this.m_tkapi.isCurrentLogined());
                if (SdkRepertory.this.m_tkapi.isCurrentLogined()) {
                    SdkRepertory.this.isStart = false;
                    SdkRepertory.this.infoShow("onConnectSuccess---current is logined");
                } else {
                    int var1;
                    if (SdkRepertory.this.lobbyStartParam != null && !SdkRepertory.this.lobbyStartParam.isEmpty()) {
                        var1 = SdkRepertory.this.m_tkapi.authUser(SdkRepertory.this.lobbyStartParam);
                    } else {
                        var1 = SdkRepertory.this.m_tkapi.autoLogin();
                    }

                    SdkRepertory.this.infoShow("lobbyStartParam :" + SdkRepertory.this.lobbyStartParam + ", login ret:" + var1);
                    if (var1 == 0) {
                        SdkRepertory.this.isStart = false;
                        return;
                    }
                }
            }

        }

        public void onDisconnect() {
            SdkRepertory.this.log("onDisconnect");
        }

        public void onMsgResp(int var1, int var2, String var3) {
            String var4 = "onMsgResp type:" + var1 + ", errCode:" + var2 + "\nresult:" + var3;
            SdkRepertory.this.log(var4);
            JSONObject var9;
            switch (var1) {
                case 3:
                case 4:
                case 5:
                case 6:
                case 24:
                case 25:
                    if (var2 == 0) {
                        try {
                            var9 = new JSONObject(var3);
                            SdkRepertory.this.figureID = var9.optInt("FigureID");
                            return;
                        } catch (JSONException var6) {
                            var6.printStackTrace();
                            return;
                        }
                    }
                    break;
                case 17:
                    if (var2 == 0) {
                        SdkRepertory.this.isPasswordExist = true;
                        return;
                    }

                    if (1021 == var2) {
                        SdkRepertory.this.isPasswordExist = false;
                        return;
                    }
                    break;
                case 22:
                    if (var2 == 0) {
                        try {
                            var9 = new JSONObject(var3);
                            SdkRepertory.this.modifyNicknameLevelID = var9.optInt("LevelID");
                            return;
                        } catch (JSONException var5) {
                            var5.printStackTrace();
                            return;
                        }
                    }
                    break;
                case 51:
                    if (var2 == 0) {
                        try {
                            JSONArray var8 = new JSONArray(var3);
                            if (var8.length() > 1) {
                                SdkRepertory.this.goodsParam = var8.getString(1);
                                return;
                            }
                        } catch (JSONException var7) {
                            var7.printStackTrace();
                            return;
                        }
                    }
                    break;
                case 53:
                    if (var2 == 0) {
                        SdkRepertory.this.payOrder(var3);
                        return;
                    }
            }

        }
    };
}
