import type { AppProps } from "next/app";




import "@/styles/globals.css";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ToastContainer } from "react-toastify";
import { useEffect, useState } from "react";
import { HeroUIProvider } from "@heroui/react";
import { ThemeProvider as NextThemesProvider, useTheme } from "next-themes";
import { useRouter } from "next/router";

import { fontSans, fontMono } from "@/config/fonts";

export default function App({ Component, pageProps }: AppProps) {
  const router = useRouter();
  const {theme} = useTheme();
  const [authorized, setAuthorized] = useState<boolean | null>(null);
  const queryClient = new QueryClient();
  useEffect(() => {
    if(authorized == null && window && window.localStorage) {
      setAuthorized(localStorage.getItem('tk') ? true : false); 
    }
  },[authorized]);
  useEffect(() => {
    setAuthorized(localStorage.getItem('tk') ? true : false); 
  },[Component,pageProps])
  return (
    <NextThemesProvider>
    <QueryClientProvider client={queryClient}>
      <HeroUIProvider navigate={router.push}>
        {router.asPath.includes("panel") ? (authorized == null ? <></> : (authorized == false ? <>Access Denied</> : <Component {...pageProps} />)) : <Component {...pageProps} /> }
        <ToastContainer theme={theme} toastStyle={{color : "gray"}} />
      </HeroUIProvider>
    </QueryClientProvider>
    </NextThemesProvider>
  );
}

export const fonts = {
  sans: fontSans.style.fontFamily,
  mono: fontMono.style.fontFamily,
};
