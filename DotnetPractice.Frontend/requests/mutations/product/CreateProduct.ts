/* eslint-disable prettier/prettier */
import axios from "axios";

import { siteConfig } from "@/config/site";

export const CreateProduct = async ({
  name,
  description,
  price,
  availableCount,
  photo,
}: {
  name: string;
  description: string;
  price: number;
  availableCount: number;
  photo: string | null;
}) => {
  const form = new FormData();
  form.append("name", name)
  if(description.trim().length != 0)
    form.append("description", description)
  form.append("price", price.toString());
  form.append("availableCount", availableCount.toString());
  if(photo) form.append('photo', photo);
  return await axios({
    url: siteConfig.requestUrls.baseUrl + siteConfig.requestUrls.product(null),
    data: form,
    method: "post",
    headers: {
        Authorization : `Bearer ${localStorage.getItem('tk')}`
    }
  });
};
