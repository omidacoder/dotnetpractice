/* eslint-disable prettier/prettier */
import axios from "axios";

import { siteConfig } from "@/config/site";

export const Login = async ({
  username,
  password,
}: {
  username: string;
  password: string;
}) => {
  return await axios({
    url: siteConfig.requestUrls.baseUrl + siteConfig.requestUrls.login,
    data: {
      UserName : username,
      Password : password,
    },
    method: "post",
  });
};
